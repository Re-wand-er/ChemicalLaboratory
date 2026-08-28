-- ============================================================
-- Скелет базы данных для PostgreSQL (без данных)
-- Все имена в snake_case, типы адаптированы
-- ============================================================

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

-- Создание базы данных, если не существует
DO $$
BEGIN
   IF NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'chemical_laboratory') THEN
      CREATE DATABASE chemical_laboratory
         WITH TEMPLATE = template0
         ENCODING = 'UTF8'
         LC_COLLATE = 'ru_RU.UTF-8'
         LC_CTYPE = 'ru_RU.UTF-8';
   END IF;
END $$;

-- Подключение к базе данных
\connect chemical_laboratory

-- Повторная установка настроек после подключения
SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';
SET default_table_access_method = heap;

-- Установка владельца базы (опционально)
ALTER DATABASE chemical_laboratory OWNER TO postgres;

-- ============================================================
-- Создание таблиц
-- ============================================================

-- 1. Категории реагентов
CREATE TABLE reagent_categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description VARCHAR(2048),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted_at TIMESTAMP
);

-- 2. График работы
CREATE TABLE work_schedule (
    id SERIAL PRIMARY KEY,
    work_shift VARCHAR(15),
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    CONSTRAINT uq_work_shift UNIQUE (start_time, end_time)
);

-- 3. Системные роли
CREATE TABLE system_roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    display_name VARCHAR(100) NOT NULL
);

-- 4. Пользователи
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    work_schedule_id INTEGER NOT NULL,
    first_name VARCHAR(20) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    middle_name VARCHAR(30) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    sex VARCHAR(2) CHECK (sex IN ('M', 'F')),
    system_role_id INTEGER NOT NULL,
    job_position VARCHAR(30),
    login VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted_at TIMESTAMP,
    CONSTRAINT fk_users_work_schedule FOREIGN KEY (work_schedule_id) REFERENCES work_schedule(id),
    CONSTRAINT fk_users_system_role FOREIGN KEY (system_role_id) REFERENCES system_roles(id)
);

-- 5. Поставщики
CREATE TABLE suppliers (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    contact_info VARCHAR(200),
    address VARCHAR(200),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    deleted_at TIMESTAMP
);

-- 6. Реагенты
CREATE TABLE reagents (
    id SERIAL PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    chemical_formula VARCHAR(100),
    unit VARCHAR(50) NOT NULL,
    current_quantity DECIMAL(10,2) NOT NULL,
    min_quantity DECIMAL(10,2) NOT NULL,
    expiration_date DATE,
    storage_location VARCHAR(100),
    category_id INTEGER NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_reagents_category FOREIGN KEY (category_id) REFERENCES reagent_categories(id)
);

-- 7. Типы операций
CREATE TABLE operation_types (
    id SERIAL PRIMARY KEY,
    code VARCHAR(50) NOT NULL UNIQUE,
    name VARCHAR(100) NOT NULL,
    affects_quantity BOOLEAN NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

-- 8. Операции с реагентами
CREATE TABLE reagent_operations (
    id SERIAL PRIMARY KEY,
    reagent_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,
    operation_type_id INTEGER NOT NULL,
    quantity DECIMAL(10,2) NOT NULL,
    operation_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    comment VARCHAR(2048),
    CONSTRAINT fk_operations_reagent FOREIGN KEY (reagent_id) REFERENCES reagents(id),
    CONSTRAINT fk_operations_user FOREIGN KEY (user_id) REFERENCES users(id),
    CONSTRAINT fk_operations_operation_type FOREIGN KEY (operation_type_id) REFERENCES operation_types(id)
);

-- 9. Поступления реагентов
CREATE TABLE reagent_receipts (
    id SERIAL PRIMARY KEY,
    reagent_id INTEGER NOT NULL,
    supplier_id INTEGER NOT NULL,
    quantity DECIMAL(10,2) NOT NULL,
    receipt_date DATE NOT NULL,
    document_number VARCHAR(100),
    CONSTRAINT fk_receipts_reagent FOREIGN KEY (reagent_id) REFERENCES reagents(id),
    CONSTRAINT fk_receipts_supplier FOREIGN KEY (supplier_id) REFERENCES suppliers(id)
);

-- 10. Настройки уведомлений
CREATE TABLE notification_settings (
    id SERIAL PRIMARY KEY,
    low_quantity_threshold DECIMAL(10,2) NOT NULL,
    expiration_days_threshold INTEGER NOT NULL,
    analytics_update_interval INTEGER NOT NULL,
    email_template VARCHAR(2048)
);

-- 11. Уведомления
CREATE TABLE notifications (
    id SERIAL PRIMARY KEY,
    reagent_id INTEGER,
    user_id INTEGER,
    notification_type VARCHAR(50) NOT NULL,
    title VARCHAR(200) NOT NULL DEFAULT '',
    message VARCHAR(2048) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP,
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    file_path VARCHAR(255),
    CONSTRAINT fk_notifications_reagent FOREIGN KEY (reagent_id) REFERENCES reagents(id),
    CONSTRAINT fk_notifications_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE SET NULL
);

-- ============================================================
-- Индексы для внешних ключей (для повышения производительности)
-- ============================================================
CREATE INDEX idx_reagents_category_id ON reagents(category_id);
CREATE INDEX idx_users_work_schedule_id ON users(work_schedule_id);
CREATE INDEX idx_users_system_role_id ON users(system_role_id);
CREATE INDEX idx_reagent_operations_reagent_id ON reagent_operations(reagent_id);
CREATE INDEX idx_reagent_operations_user_id ON reagent_operations(user_id);
CREATE INDEX idx_reagent_operations_operation_type_id ON reagent_operations(operation_type_id);
CREATE INDEX idx_reagent_receipts_reagent_id ON reagent_receipts(reagent_id);
CREATE INDEX idx_reagent_receipts_supplier_id ON reagent_receipts(supplier_id);
CREATE INDEX idx_notifications_reagent_id ON notifications(reagent_id);
CREATE INDEX idx_notifications_user_id ON notifications(user_id);