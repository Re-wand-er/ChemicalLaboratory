--
-- PostgreSQL database cluster dump
--

-- Started on 2026-08-12 20:29:17

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

-- CREATE ROLE postgres;
-- ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;
-- CREATE ROLE "testdb$testdb$0300000000000000";
-- ALTER ROLE "testdb$testdb$0300000000000000" WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$0800000000000000";
-- ALTER ROLE "testdb$testdb$0800000000000000" WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$0d00000000000000";
-- ALTER ROLE "testdb$testdb$0d00000000000000" WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$atlantis";
-- ALTER ROLE "testdb$testdb$atlantis" WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$galaktika";
-- ALTER ROLE "testdb$testdb$galaktika" WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$grp$curators";
-- ALTER ROLE "testdb$testdb$grp$curators" WITH NOSUPERUSER INHERIT CREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$postgres";
-- ALTER ROLE "testdb$testdb$postgres" WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$public";
-- ALTER ROLE "testdb$testdb$public" WITH NOSUPERUSER INHERIT NOCREATEROLE NOCREATEDB NOLOGIN NOREPLICATION NOBYPASSRLS;
-- CREATE ROLE "testdb$testdb$user";
-- ALTER ROLE "testdb$testdb$user" WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS;

--
-- User Configurations
--


--
-- Role memberships
--

-- GRANT "testdb$testdb$0300000000000000" TO "testdb$testdb$galaktika" WITH INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$0800000000000000" TO "testdb$testdb$postgres" WITH INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$0d00000000000000" TO "testdb$testdb$user" WITH INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$grp$curators" TO "testdb$testdb$atlantis" WITH ADMIN OPTION, INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$public" TO "testdb$testdb$0300000000000000" WITH INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$public" TO "testdb$testdb$0800000000000000" WITH INHERIT TRUE GRANTED BY postgres;
-- GRANT "testdb$testdb$public" TO "testdb$testdb$0d00000000000000" WITH INHERIT TRUE GRANTED BY postgres;






--
-- Databases
--

--
-- Database "template1" dump
--

\connect template1

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2026-08-12 20:29:17

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

-- Completed on 2026-08-12 20:29:17

--
-- PostgreSQL database dump complete
--

--
-- Database "ChemicalLaboratory" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2026-08-12 20:29:17

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

--
-- TOC entry 4841 (class 1262 OID 122727)
-- Name: ChemicalLaboratory; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "ChemicalLaboratory" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'ru-RU';


ALTER DATABASE "ChemicalLaboratory" OWNER TO postgres;

\connect "ChemicalLaboratory"

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

--
-- TOC entry 225 (class 1259 OID 122760)
-- Name: notifications; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Notifications" (
   "Id" integer NOT NULL,
    "ReagentId" integer,
    "NotificationType" character varying(50),
    "Message" character varying(2048),
    "CreatedAt" timestamp without time zone NOT NULL,
    "IsRead" boolean NOT NULL,
    "Title" character varying(200),
    "UserId" integer,
    "DeletedAt" timestamp without time zone,
    "FilePath" character varying(255)
);


ALTER TABLE public."Notifications" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 122728)
-- Name: "NotificationSettings"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."NotificationSettings" (
   "Id" integer NOT NULL,
    "LowQuantityThreshold" numeric(10,2),
    "ExpirationDaysThreshold" integer NOT NULL,
    "AnalyticsUpdateInterval" integer NOT NULL,
    "EmailTemplate" character varying(2048)
);


ALTER TABLE public."NotificationSettings" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 122733)
-- Name: "OperationTypes"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OperationTypes" (
   "Id" integer NOT NULL,
    "Code" character varying(50),
    "Name" character varying(100),
    "AffectsQuantity" boolean NOT NULL,
    "IsActive" boolean NOT NULL
);


ALTER TABLE public."OperationTypes" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 122736)
-- Name: "ReagentCategories"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReagentCategories" (
   "Id" integer NOT NULL,
    "Name" character varying(100),
    "Description" character varying(2048),
    "IsActive" boolean NOT NULL,
    "DeletedAt" timestamp without time zone
);


ALTER TABLE public."ReagentCategories" OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 122765)
-- Name: "ReagentOperations"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReagentOperations" (
   "Id" integer NOT NULL,
    "ReagentId" integer NOT NULL,
    "UserId" integer NOT NULL,
    "OperationTypeId" integer NOT NULL,
    "Quantity" numeric(10,2),
    "OperationDate" timestamp without time zone NOT NULL,
    "Comment" character varying(2048)
);


ALTER TABLE public."ReagentOperations" OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 122770)
-- Name: "ReagentReceipts" ; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ReagentReceipts"  (
   "Id" integer NOT NULL,
    "ReagentId" integer NOT NULL,
    "SupplierId" integer NOT NULL,
    "Quantity" numeric(10,2),
    "ReceiptDate" date NOT NULL,
    "DocumentNumber" character varying(100)
);


ALTER TABLE public."ReagentReceipts"  OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 122752)
-- Name: reagents; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Reagents"  (
   "Id" integer NOT NULL,
    "Name" character varying(200),
    "ChemicalFormula" character varying(100),
    "Unit" character varying(50),
    "CurrentQuantity" numeric(10,2),
    "MinQuantity" numeric(10,2),
    "ExpirationDate" date,
    "StorageLocation" character varying(100),
    "CategoryId" integer NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "IsActive" boolean NOT NULL,
    "DeletedAt" timestamp without time zone
);


ALTER TABLE public."Reagents"  OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 122741)
-- Name: "Suppliers"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Suppliers" (
   "Id" integer NOT NULL,
    "Name" character varying(200),
    "ContactInfo" character varying(200),
    "Address" character varying(200),
    "IsActive" boolean NOT NULL,
    "DeletedAt" timestamp without time zone
);


ALTER TABLE public."Suppliers" OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 122746)
-- Name: "SystemRoles"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SystemRoles" (
   "Id" integer NOT NULL,
    "Name" character varying(50),
    "DisplayName" character varying(100)
);


ALTER TABLE public."SystemRoles" OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 122755)
-- Name: "Users"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
   "Id" integer NOT NULL,
   "IdWorkSchedule" integer NOT NULL,
    "FirstName" character varying(20),
    "MiddleName" character varying(30),
    "LastName" character varying(50),
    "Email" character varying(150),
    "Sex" character varying(2),
    "SystemRoleId" integer,
    "JobPosition" character varying(30),
    "Login" character varying(255),
    "PasswordHash" character varying(255),
    "IsActive" boolean NOT NULL,
    "DeletedAt" timestamp without time zone
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 122749)
-- Name: "WorkSchedule"; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."WorkSchedule" (
   "Id" integer NOT NULL,
    "WorkShift" character varying(15),
    "StartTime" time without time zone NOT NULL,
    "EndTime" time without time zone NOT NULL
);


ALTER TABLE public."WorkSchedule" OWNER TO postgres;

--
-- TOC entry 4833 (class 0 OID 122760)
-- Dependencies: 225
-- Data for Name: notifications; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Notifications" ("Id", "ReagentId", "NotificationType", "Message", "CreatedAt", "IsRead", "Title", "UserId", "DeletedAt", "FilePath") FROM stdin;
1	2	LOW_QUANTITY	Остаток реагента ниже минимального порога	2026-01-17 18:11:44.51	true	Заголовок	8	2026-05-17 17:48:46.42	\N
2	4	EXPIRATION	Срок годности истекает менее чем через 30 дней	2026-01-17 18:11:44.51	true	Заголовок	8	2026-05-16 17:47:33	\N
3	2	EXPIRED	Реагент имеет истекший срок годности	2026-01-17 18:11:44.51	true	Заголовок	7	\N	\N
4	4	LOW_QUANTITY	Остаток мог бы быть и больше	2026-01-19 18:11:44.51	false	Заголовок	8	\N	\N
14	51	LowQuantity	Реактив "Серная кислота" имеет остаток 2.5 кг при минимальном пороге 5 кг. Рекомендуется заказать поставку.	2026-04-15 18:43:24.68	false	Низкий остаток реактива	8	\N	\N
15	2	LowQuantity	Реактив "Серная кислота" имеет остаток 2.5 кг при минимальном пороге 5 кг. Рекомендуется заказать поставку.	2026-04-15 18:43:30.17	true	Низкий остаток реактива	8	\N	\N
16	16	LowQuantity	Реактив "Серная кислота" имеет остаток 2.5 кг при минимальном пороге 5 кг. Рекомендуется заказать поставку.	2026-04-15 18:43:34.56	true	Низкий остаток реактива	8	2026-05-24 13:49:08.46	\N
18	41	LowQuantity	Низкое количество меди. Необходимо пополнить запас до 11.07.2026.	2026-04-15 18:43:52.8	false	Низкий остаток реактива	8	2026-05-24 14:51:27.18	\N
20	25	LowQuantity	Реактив "Серная кислота" имеет остаток 2.5 кг при минимальном пороге 5 кг. Рекомендуется заказать поставку.	2026-04-15 18:44:06.48	false	Низкий остаток реактива	8	2026-05-24 13:52:50.63	\N
21	51	LowQuantity	АВТОМАТ	2026-04-15 18:56:34.7	true	АВТОМАТИЧЕСКОЕ ОБНОВЛЕНИЕ	8	\N	\N
23	1	Low_Quantity	Низкое количество реактива Серная кислота. Необходимо пополнить запас до 26.03.2026 0:00:00	2026-05-16 18:51:26	false	Низкий остаток реактива Серная кислота	8	2026-05-24 13:44:35.38	\N
24	1	Low_Quantity	Низкое количество реактива Серная кислота. Необходимо пополнить запас до 26.03.2026 0:00:00	2026-05-16 19:01:15.3	false	Низкий остаток реактива Серная кислота	8	2026-05-24 13:04:05.51	\N
25	1	Low_Quantity	Низкое количество реактива Серная кислота.Необходимо пополнить запас до 26.03.2026	2026-05-17 17:49:07.02	true	Низкий остаток реактива Серная кислота	8	\N	\N
26	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 15:13:00.09	false	Обнаружен дефицит реагентов	8	2026-05-18 17:12:00.23	/invoices/invoice_low_stock_20260518_181250.pdf
27	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 15:24:42.98	false	Обнаружен дефицит реагентов	8	2026-05-18 17:40:15.91	/invoices/invoice_low_stock_20260518_182442.pdf
28	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:10:23.92	false	Обнаружен дефицит реагентов	8	2026-05-18 17:11:56.94	/invoices/invoice_low_stock_20260518_201023.pdf
29	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:11:05.45	false	Обнаружен дефицит реагентов	8	2026-05-18 17:11:56.26	/invoices/invoice_low_stock_20260518_201105.pdf
30	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:11:05.66	false	Обнаружен дефицит реагентов	8	2026-05-18 17:11:55.03	/invoices/invoice_low_stock_20260518_201105.pdf
31	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:11:05.72	false	Обнаружен дефицит реагентов	8	2026-05-18 17:11:55.55	/invoices/invoice_low_stock_20260518_201105.pdf
32	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:13:54.49	false	Обнаружен дефицит реагентов	8	2026-05-18 17:40:16.67	/invoices/invoice_low_stock_20260518_201354.pdf
33	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:13:54.58	false	Обнаружен дефицит реагентов	8	2026-05-18 17:40:18.22	/invoices/invoice_low_stock_20260518_201354.pdf
34	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:25:25.61	false	Обнаружен дефицит реагентов	8	2026-05-18 17:40:21.74	/invoices/invoice_low_stock_20260518_202525.pdf
35	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:26:27.61	false	Обнаружен дефицит реагентов	8	2026-05-18 17:53:38.86	/invoices/invoice_low_stock_20260518_202627.pdf
36	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:39:46.68	false	Обнаружен дефицит реагентов	8	2026-05-18 17:53:37.31	/invoices/invoice_low_stock_20260518_203946.pdf
37	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:40:37.3	false	Обнаружен дефицит реагентов	8	2026-05-18 17:53:36.63	/invoices/invoice_low_stock_20260518_204037.pdf
38	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:48:15.24	false	Обнаружен дефицит реагентов	8	2026-05-18 17:53:35.48	/invoices/invoice_low_stock_20260518_204815.pdf
39	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:51:58.01	false	Обнаружен дефицит реагентов	8	2026-05-18 17:53:40.14	/invoices/invoice_low_stock_20260518_205157.pdf
40	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-18 17:53:11.6	false	Обнаружен дефицит реагентов	8	2026-05-21 15:02:32.94	/invoices/invoice_low_stock_20260518_205311.pdf
41	9	Low_Quantity	Низкое количество реактива Уксусная кислота.Необходимо пополнить запас до 31.12.2026	2026-05-21 09:19:59.2	true	Низкий остаток реактива Уксусная кислота	8	\N	\N
42	11	Low_Quantity	Низкое количество реактива Аммиак.Необходимо пополнить запас до 01.06.2025	2026-05-21 09:20:35.26	true	Низкий остаток реактива Аммиак	8	\N	\N
43	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-21 14:57:21.39	false	Обнаружен дефицит реагентов	8	2026-05-21 14:57:46.45	/invoices/invoice_low_stock_20260521_175721.pdf
44	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-21 14:58:05.41	false	Обнаружен дефицит реагентов	8	2026-05-21 15:02:31.05	/invoices/invoice_low_stock_20260521_175805.pdf
45	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-21 15:02:17.82	false	Обнаружен дефицит реагентов	8	2026-05-21 15:04:20.69	/invoices/invoice_low_stock_20260521_180217.pdf
46	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-21 15:04:10.88	false	Обнаружен дефицит реагентов	8	2026-05-22 17:31:37.08	/invoices/invoice_low_stock_20260521_180410.pdf
47	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-22 16:57:03.98	false	Обнаружен дефицит реагентов	8	2026-05-22 17:31:36.71	/invoices/invoice_low_stock_20260522_195703.pdf
48	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-22 16:57:21.31	false	Обнаружен дефицит реагентов	8	2026-05-22 17:31:35.79	/invoices/invoice_low_stock_20260522_195721.pdf
49	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-22 16:59:58.09	false	Обнаружен дефицит реагентов	8	2026-05-22 17:31:40.27	/invoices/invoice_low_stock_20260522_195958.pdf
50	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-22 17:01:49.36	false	Обнаружен дефицит реагентов	8	\N	/invoices/invoice_low_stock_20260522_200149.pdf
51	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-24 13:05:29.41	false	Обнаружен дефицит реагентов	8	2026-05-24 13:10:34.34	/invoices/invoice_low_stock_20260524_160528.pdf
52	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-24 13:50:34.15	false	Обнаружен дефицит реагентов	8	\N	/invoices/invoice_low_stock_20260524_165033.pdf
53	\N	Low_Quantity	Сформирована накладная на закупку.	2026-05-24 13:54:11.4	false	Обнаружен дефицит реагентов	8	\N	/invoices/invoice_low_stock_20260524_165411.pdf
\.


--
-- TOC entry 4825 (class 0 OID 122728)
-- Dependencies: 217
-- Data for Name: "NotificationSettings"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."NotificationSettings" ("Id", "LowQuantityThreshold", "ExpirationDaysThreshold", "AnalyticsUpdateInterval", "EmailTemplate") FROM stdin;
1	10.00	30	24	Уведомление: реагент {ReagentName} требует внимания
\.


--
-- TOC entry 4826 (class 0 OID 122733)
-- Dependencies: 218
-- Data for Name: "OperationTypes"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."OperationTypes" ("Id", "Code", "Name", "AffectsQuantity", "IsActive") FROM stdin;
1	RECEIPT	Поступление	true	true
2	CONSUMPTION	Расход	true	true
3	WRITEOFF	Списание	true	true
4	ADJUSTMENT	Корректировка	true	true
5	UPDATE	Изменение данных	false	true
\.


--
-- TOC entry 4827 (class 0 OID 122736)
-- Dependencies: 219
-- Data for Name: "ReagentCategories"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ReagentCategories" ("Id", "Name", "Description", "IsActive", "DeletedAt") FROM stdin;
1	Кислоты	Неорганические и органические кислоты	true	\N
2	Щелочи	Щелочные реагенты	true	\N
3	Растворители	Органические растворители	true	\N
4	Соли	Неорганические соли	true	\N
5	Оксиды	Соединения элементов с водой	true	\N
6	Индикаторы	Вещества для определения среды раствора	true	\N
7	Буферные растворы	Смеси, поддерживающие стабильный уровень pH при добавлении кислот или щелочей	true	\N
8	Металлы и неметаллы	Простые вещества в виде порошков, гранул или листов	true	\N
\.


--
-- TOC entry 4834 (class 0 OID 122765)
-- Dependencies: 226
-- Data for Name: "ReagentOperations"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ReagentOperations" ("Id", "ReagentId", "UserId", "OperationTypeId", "Quantity", "OperationDate", "Comment") FROM stdin;
1	1	5	1	10.00	2026-04-03 15:00:19.11	Поступление от поставщика
3	4	7	2	5.00	2026-04-03 15:00:19.11	Расход за неделю
4	3	5	3	-1.00	2026-04-03 15:00:19.11	Корректировка после инвентаризации
306	8	6	3	-3.77	2026-03-27 10:47:20.42	Генерация: "Reagents"  1-5, 8-15, 25
307	8	6	3	-39.50	2025-06-06 07:53:44.42	Генерация: "Reagents"  1-5, 8-15, 25
308	8	6	2	-30.53	2026-02-16 01:23:33.42	Генерация: "Reagents"  1-5, 8-15, 25
309	8	6	3	-7.32	2025-02-18 14:22:12.42	Генерация: "Reagents"  1-5, 8-15, 25
310	8	8	2	39.48	2025-11-18 16:03:08.42	Генерация: "Reagents"  1-5, 8-15, 25
311	8	7	5	27.66	2025-05-25 20:58:03.42	Генерация: "Reagents"  1-5, 8-15, 25
312	8	7	3	2.71	2025-12-18 05:36:28.42	Генерация: "Reagents"  1-5, 8-15, 25
313	8	6	1	24.37	2025-10-21 08:13:34.42	Генерация: "Reagents"  1-5, 8-15, 25
314	8	8	3	-19.02	2025-12-23 13:37:09.42	Генерация: "Reagents"  1-5, 8-15, 25
315	8	7	1	35.73	2024-12-04 11:35:33.42	Генерация: "Reagents"  1-5, 8-15, 25
316	8	8	5	25.53	2024-04-18 15:45:21.42	Генерация: "Reagents"  1-5, 8-15, 25
317	8	6	4	47.21	2025-10-20 21:49:47.42	Генерация: "Reagents"  1-5, 8-15, 25
318	8	6	2	-31.35	2025-07-22 14:44:35.42	Генерация: "Reagents"  1-5, 8-15, 25
319	8	8	1	11.37	2026-02-05 19:49:04.42	Генерация: "Reagents"  1-5, 8-15, 25
320	8	5	4	2.01	2025-02-26 18:29:31.42	Генерация: "Reagents"  1-5, 8-15, 25
321	8	8	2	94.65	2024-06-22 14:39:30.42	Генерация: "Reagents"  1-5, 8-15, 25
322	8	8	4	67.40	2025-05-15 14:08:04.42	Генерация: "Reagents"  1-5, 8-15, 25
323	8	8	2	15.56	2025-03-22 16:59:25.42	Генерация: "Reagents"  1-5, 8-15, 25
324	8	8	1	30.17	2024-05-20 22:41:24.42	Генерация: "Reagents"  1-5, 8-15, 25
325	8	5	5	34.59	2024-12-30 20:33:26.42	Генерация: "Reagents"  1-5, 8-15, 25
326	8	5	3	-8.78	2025-05-28 21:14:36.42	Генерация: "Reagents"  1-5, 8-15, 25
327	8	7	3	37.24	2024-08-02 12:23:14.42	Генерация: "Reagents"  1-5, 8-15, 25
328	8	7	3	17.86	2025-07-29 17:32:44.42	Генерация: "Reagents"  1-5, 8-15, 25
329	8	5	4	72.57	2026-02-15 17:10:16.42	Генерация: "Reagents"  1-5, 8-15, 25
330	8	7	1	90.77	2025-08-07 21:35:59.42	Генерация: "Reagents"  1-5, 8-15, 25
331	8	7	1	49.46	2025-04-23 12:15:16.42	Генерация: "Reagents"  1-5, 8-15, 25
332	8	5	3	-33.62	2026-03-22 09:28:32.42	Генерация: "Reagents"  1-5, 8-15, 25
333	8	7	3	10.13	2025-09-04 03:16:18.42	Генерация: "Reagents"  1-5, 8-15, 25
334	8	7	3	-39.73	2025-04-02 10:55:56.42	Генерация: "Reagents"  1-5, 8-15, 25
335	8	7	5	-9.39	2025-04-25 08:02:19.42	Генерация: "Reagents"  1-5, 8-15, 25
336	8	5	1	21.09	2025-04-30 13:02:59.42	Генерация: "Reagents"  1-5, 8-15, 25
337	8	6	4	7.60	2025-12-21 17:35:41.42	Генерация: "Reagents"  1-5, 8-15, 25
338	8	6	4	-43.10	2024-05-23 19:27:40.42	Генерация: "Reagents"  1-5, 8-15, 25
339	8	5	3	-45.55	2024-11-27 21:57:23.42	Генерация: "Reagents"  1-5, 8-15, 25
340	8	6	2	-11.48	2025-10-19 03:04:48.42	Генерация: "Reagents"  1-5, 8-15, 25
341	8	7	1	36.37	2025-11-28 06:53:36.42	Генерация: "Reagents"  1-5, 8-15, 25
342	8	6	3	-20.41	2025-11-08 06:03:35.42	Генерация: "Reagents"  1-5, 8-15, 25
343	8	6	1	41.10	2024-07-02 10:14:45.42	Генерация: "Reagents"  1-5, 8-15, 25
344	8	6	1	79.36	2024-04-29 04:42:43.42	Генерация: "Reagents"  1-5, 8-15, 25
345	8	7	4	-30.70	2026-02-05 23:36:01.42	Генерация: "Reagents"  1-5, 8-15, 25
346	8	5	4	69.04	2025-05-17 06:04:21.42	Генерация: "Reagents"  1-5, 8-15, 25
347	8	6	3	-6.10	2025-12-27 04:56:35.42	Генерация: "Reagents"  1-5, 8-15, 25
348	8	7	3	9.74	2025-04-16 03:32:28.42	Генерация: "Reagents"  1-5, 8-15, 25
349	8	5	4	28.55	2024-11-13 18:15:03.42	Генерация: "Reagents"  1-5, 8-15, 25
350	8	8	3	92.04	2024-07-06 23:32:01.42	Генерация: "Reagents"  1-5, 8-15, 25
351	8	6	4	-16.19	2024-09-05 06:57:11.42	Генерация: "Reagents"  1-5, 8-15, 25
352	8	5	3	62.04	2024-10-07 13:17:06.42	Генерация: "Reagents"  1-5, 8-15, 25
353	8	6	4	62.21	2024-05-11 05:39:21.42	Генерация: "Reagents"  1-5, 8-15, 25
354	8	5	2	-35.14	2025-02-03 00:28:54.42	Генерация: "Reagents"  1-5, 8-15, 25
355	8	8	5	33.21	2025-11-29 15:14:00.42	Генерация: "Reagents"  1-5, 8-15, 25
356	8	8	4	95.11	2024-04-22 23:32:01.42	Генерация: "Reagents"  1-5, 8-15, 25
357	8	6	1	88.12	2024-07-11 08:25:17.42	Генерация: "Reagents"  1-5, 8-15, 25
358	8	7	1	40.93	2024-04-18 21:27:12.42	Генерация: "Reagents"  1-5, 8-15, 25
359	8	6	3	70.79	2026-03-25 02:01:41.42	Генерация: "Reagents"  1-5, 8-15, 25
360	8	8	2	-42.46	2024-04-19 14:33:44.42	Генерация: "Reagents"  1-5, 8-15, 25
361	8	8	4	-45.21	2025-11-27 02:13:44.42	Генерация: "Reagents"  1-5, 8-15, 25
362	8	7	5	-13.19	2025-04-04 20:46:05.42	Генерация: "Reagents"  1-5, 8-15, 25
363	8	6	4	2.36	2025-10-15 23:47:08.42	Генерация: "Reagents"  1-5, 8-15, 25
364	8	6	4	6.33	2025-03-14 15:54:02.42	Генерация: "Reagents"  1-5, 8-15, 25
365	8	8	4	72.79	2025-01-21 16:06:53.42	Генерация: "Reagents"  1-5, 8-15, 25
366	8	6	1	21.80	2024-10-09 22:50:42.42	Генерация: "Reagents"  1-5, 8-15, 25
367	8	5	1	41.58	2025-12-31 11:09:27.42	Генерация: "Reagents"  1-5, 8-15, 25
368	8	7	2	70.49	2026-03-17 22:30:11.42	Генерация: "Reagents"  1-5, 8-15, 25
369	8	8	2	62.85	2025-08-04 19:12:31.42	Генерация: "Reagents"  1-5, 8-15, 25
370	8	5	2	98.98	2025-04-12 20:31:48.42	Генерация: "Reagents"  1-5, 8-15, 25
371	8	6	3	91.17	2026-03-20 07:30:36.42	Генерация: "Reagents"  1-5, 8-15, 25
372	8	7	3	26.75	2025-01-20 19:32:38.42	Генерация: "Reagents"  1-5, 8-15, 25
373	8	7	1	93.61	2025-11-05 08:17:23.42	Генерация: "Reagents"  1-5, 8-15, 25
374	8	5	3	10.42	2026-02-20 14:34:07.42	Генерация: "Reagents"  1-5, 8-15, 25
375	8	7	2	-21.96	2025-12-05 22:08:40.42	Генерация: "Reagents"  1-5, 8-15, 25
376	8	8	5	70.27	2024-09-23 17:54:55.42	Генерация: "Reagents"  1-5, 8-15, 25
377	8	6	2	-41.60	2025-12-27 16:34:36.42	Генерация: "Reagents"  1-5, 8-15, 25
378	8	5	3	55.69	2025-08-12 16:31:46.42	Генерация: "Reagents"  1-5, 8-15, 25
379	8	6	5	24.21	2025-02-24 04:28:11.42	Генерация: "Reagents"  1-5, 8-15, 25
380	8	8	1	41.55	2025-08-20 00:38:18.42	Генерация: "Reagents"  1-5, 8-15, 25
381	8	8	3	-6.43	2025-06-14 18:22:19.42	Генерация: "Reagents"  1-5, 8-15, 25
382	8	8	5	-46.49	2025-01-06 14:48:11.42	Генерация: "Reagents"  1-5, 8-15, 25
383	8	6	3	33.67	2025-10-14 06:58:26.42	Генерация: "Reagents"  1-5, 8-15, 25
384	8	5	2	-18.25	2024-05-21 13:30:32.42	Генерация: "Reagents"  1-5, 8-15, 25
385	8	5	3	27.30	2024-08-17 02:24:35.42	Генерация: "Reagents"  1-5, 8-15, 25
386	8	8	4	59.37	2026-03-15 19:28:40.42	Генерация: "Reagents"  1-5, 8-15, 25
387	8	8	1	61.51	2024-11-19 13:44:47.42	Генерация: "Reagents"  1-5, 8-15, 25
388	8	7	2	59.07	2025-09-02 08:15:39.42	Генерация: "Reagents"  1-5, 8-15, 25
389	8	7	4	62.64	2025-01-08 17:00:59.42	Генерация: "Reagents"  1-5, 8-15, 25
390	8	6	4	6.07	2024-11-15 15:33:44.42	Генерация: "Reagents"  1-5, 8-15, 25
391	8	6	3	21.40	2025-12-20 14:54:35.42	Генерация: "Reagents"  1-5, 8-15, 25
392	8	7	2	-21.83	2025-02-08 16:10:13.42	Генерация: "Reagents"  1-5, 8-15, 25
393	8	5	1	41.95	2025-07-28 19:59:20.42	Генерация: "Reagents"  1-5, 8-15, 25
394	8	8	2	-41.32	2024-07-06 21:33:35.42	Генерация: "Reagents"  1-5, 8-15, 25
395	8	8	5	-14.42	2024-10-21 00:44:42.42	Генерация: "Reagents"  1-5, 8-15, 25
396	8	7	1	4.01	2024-04-17 01:07:09.42	Генерация: "Reagents"  1-5, 8-15, 25
397	8	7	4	55.37	2024-06-22 01:01:46.42	Генерация: "Reagents"  1-5, 8-15, 25
398	8	7	1	8.44	2024-04-22 23:39:21.42	Генерация: "Reagents"  1-5, 8-15, 25
399	8	8	5	9.26	2024-04-30 08:41:23.42	Генерация: "Reagents"  1-5, 8-15, 25
400	8	8	2	-48.18	2025-07-10 09:14:52.42	Генерация: "Reagents"  1-5, 8-15, 25
401	8	8	1	58.47	2025-01-15 22:16:22.42	Генерация: "Reagents"  1-5, 8-15, 25
402	8	5	1	38.22	2024-10-21 17:25:24.42	Генерация: "Reagents"  1-5, 8-15, 25
403	8	7	3	72.84	2025-09-09 09:45:17.42	Генерация: "Reagents"  1-5, 8-15, 25
404	8	5	2	81.62	2026-01-23 22:14:02.42	Генерация: "Reagents"  1-5, 8-15, 25
405	8	8	3	19.03	2025-08-03 21:31:58.42	Генерация: "Reagents"  1-5, 8-15, 25
406	8	7	3	-6.78	2025-06-05 11:50:38.42	Генерация: "Reagents"  1-5, 8-15, 25
407	8	6	4	74.61	2025-07-12 08:34:44.42	Генерация: "Reagents"  1-5, 8-15, 25
408	8	8	1	20.33	2026-04-06 18:13:15.42	Генерация: "Reagents"  1-5, 8-15, 25
409	8	5	4	29.74	2025-09-25 13:22:10.42	Генерация: "Reagents"  1-5, 8-15, 25
410	8	8	5	88.96	2025-05-09 15:20:36.42	Генерация: "Reagents"  1-5, 8-15, 25
411	8	5	2	76.36	2025-03-19 11:26:31.42	Генерация: "Reagents"  1-5, 8-15, 25
412	8	8	1	12.00	2025-08-19 16:26:36.42	Генерация: "Reagents"  1-5, 8-15, 25
413	8	6	1	80.96	2024-11-27 10:33:01.42	Генерация: "Reagents"  1-5, 8-15, 25
414	8	6	1	5.10	2025-04-22 20:39:48.42	Генерация: "Reagents"  1-5, 8-15, 25
415	8	6	3	-36.76	2025-03-20 07:41:15.42	Генерация: "Reagents"  1-5, 8-15, 25
416	8	6	4	28.02	2024-09-30 11:46:09.42	Генерация: "Reagents"  1-5, 8-15, 25
417	8	7	5	-23.35	2025-07-20 23:16:47.42	Генерация: "Reagents"  1-5, 8-15, 25
418	8	5	1	87.42	2024-06-18 13:48:03.42	Генерация: "Reagents"  1-5, 8-15, 25
419	8	5	5	8.60	2025-05-15 06:34:28.42	Генерация: "Reagents"  1-5, 8-15, 25
420	8	7	2	5.90	2024-12-19 05:13:01.42	Генерация: "Reagents"  1-5, 8-15, 25
421	8	8	3	71.88	2026-01-04 14:19:54.42	Генерация: "Reagents"  1-5, 8-15, 25
422	8	7	5	-4.34	2025-03-19 02:43:05.42	Генерация: "Reagents"  1-5, 8-15, 25
423	8	6	2	2.46	2025-05-18 00:05:35.42	Генерация: "Reagents"  1-5, 8-15, 25
424	8	8	4	4.44	2025-12-16 05:01:39.42	Генерация: "Reagents"  1-5, 8-15, 25
425	8	6	3	86.39	2024-10-29 20:15:35.42	Генерация: "Reagents"  1-5, 8-15, 25
426	8	5	5	-42.71	2026-04-01 06:00:57.42	Генерация: "Reagents"  1-5, 8-15, 25
427	8	5	2	15.05	2025-07-10 18:24:29.42	Генерация: "Reagents"  1-5, 8-15, 25
428	8	7	3	34.28	2025-05-14 05:36:27.42	Генерация: "Reagents"  1-5, 8-15, 25
429	8	6	5	17.37	2024-11-01 18:50:12.42	Генерация: "Reagents"  1-5, 8-15, 25
430	8	6	5	87.40	2024-06-05 02:49:06.42	Генерация: "Reagents"  1-5, 8-15, 25
431	8	8	1	25.87	2024-04-22 05:37:41.42	Генерация: "Reagents"  1-5, 8-15, 25
432	8	7	5	27.68	2026-03-03 05:03:56.42	Генерация: "Reagents"  1-5, 8-15, 25
433	8	7	4	97.31	2024-11-17 06:30:20.42	Генерация: "Reagents"  1-5, 8-15, 25
434	8	7	1	18.18	2024-07-24 17:12:03.42	Генерация: "Reagents"  1-5, 8-15, 25
435	8	8	1	21.53	2025-02-22 14:55:25.42	Генерация: "Reagents"  1-5, 8-15, 25
436	8	8	4	34.02	2026-04-06 04:37:47.42	Генерация: "Reagents"  1-5, 8-15, 25
437	8	5	2	44.03	2025-05-08 18:13:10.42	Генерация: "Reagents"  1-5, 8-15, 25
438	8	8	2	10.49	2025-06-11 01:09:37.42	Генерация: "Reagents"  1-5, 8-15, 25
439	8	6	3	-22.05	2026-03-24 21:14:40.42	Генерация: "Reagents"  1-5, 8-15, 25
440	8	7	5	-3.09	2024-07-18 13:46:41.42	Генерация: "Reagents"  1-5, 8-15, 25
441	8	8	4	79.64	2025-09-22 03:56:47.42	Генерация: "Reagents"  1-5, 8-15, 25
442	8	8	5	68.27	2025-11-10 19:00:42.42	Генерация: "Reagents"  1-5, 8-15, 25
443	8	7	4	54.14	2025-02-04 20:03:04.42	Генерация: "Reagents"  1-5, 8-15, 25
444	8	8	2	59.80	2024-04-21 13:54:16.42	Генерация: "Reagents"  1-5, 8-15, 25
445	8	6	2	39.92	2026-03-16 15:12:09.42	Генерация: "Reagents"  1-5, 8-15, 25
446	8	7	2	11.96	2024-07-25 17:41:24.42	Генерация: "Reagents"  1-5, 8-15, 25
447	8	8	3	21.97	2024-06-14 04:20:50.42	Генерация: "Reagents"  1-5, 8-15, 25
448	8	6	1	17.98	2025-04-03 22:04:21.42	Генерация: "Reagents"  1-5, 8-15, 25
449	8	5	1	77.48	2024-07-20 12:44:53.42	Генерация: "Reagents"  1-5, 8-15, 25
450	8	8	4	-26.87	2024-06-12 22:36:53.42	Генерация: "Reagents"  1-5, 8-15, 25
451	8	5	4	10.24	2026-03-12 10:18:04.42	Генерация: "Reagents"  1-5, 8-15, 25
452	8	8	2	-47.75	2025-01-02 17:17:20.42	Генерация: "Reagents"  1-5, 8-15, 25
453	8	6	2	38.48	2026-03-06 06:33:50.42	Генерация: "Reagents"  1-5, 8-15, 25
454	8	5	5	68.40	2026-02-04 04:53:04.42	Генерация: "Reagents"  1-5, 8-15, 25
455	8	8	3	-31.60	2025-04-12 13:19:13.42	Генерация: "Reagents"  1-5, 8-15, 25
456	3	6	3	-6.27	2026-01-14 02:49:04.38	Генерация: "Reagents"  1-5, 8-15, 25
457	3	5	1	20.11	2024-10-22 21:08:01.38	Генерация: "Reagents"  1-5, 8-15, 25
458	3	5	4	-19.76	2025-02-20 15:36:27.38	Генерация: "Reagents"  1-5, 8-15, 25
459	3	5	3	3.53	2024-04-17 15:31:27.38	Генерация: "Reagents"  1-5, 8-15, 25
460	3	7	2	-27.16	2026-01-13 22:23:12.38	Генерация: "Reagents"  1-5, 8-15, 25
461	3	6	5	79.05	2025-07-28 01:45:21.38	Генерация: "Reagents"  1-5, 8-15, 25
462	3	8	2	-33.86	2025-04-27 16:38:45.38	Генерация: "Reagents"  1-5, 8-15, 25
463	3	5	4	64.28	2024-12-30 14:45:51.38	Генерация: "Reagents"  1-5, 8-15, 25
464	3	5	2	6.37	2025-07-28 05:31:28.38	Генерация: "Reagents"  1-5, 8-15, 25
465	3	8	2	37.37	2025-09-25 05:14:30.38	Генерация: "Reagents"  1-5, 8-15, 25
466	3	8	3	57.79	2025-12-23 07:26:15.38	Генерация: "Reagents"  1-5, 8-15, 25
467	3	7	5	-18.42	2025-12-02 01:59:24.38	Генерация: "Reagents"  1-5, 8-15, 25
468	3	5	1	15.40	2024-07-07 16:54:29.38	Генерация: "Reagents"  1-5, 8-15, 25
469	3	8	4	68.98	2024-07-06 17:47:57.38	Генерация: "Reagents"  1-5, 8-15, 25
470	3	7	2	5.52	2026-02-28 21:52:31.38	Генерация: "Reagents"  1-5, 8-15, 25
471	3	5	2	0.75	2025-03-05 19:08:16.38	Генерация: "Reagents"  1-5, 8-15, 25
472	3	5	1	9.31	2026-01-23 18:28:33.38	Генерация: "Reagents"  1-5, 8-15, 25
473	3	5	2	78.46	2025-03-11 06:19:24.38	Генерация: "Reagents"  1-5, 8-15, 25
474	3	8	1	58.08	2025-01-27 16:25:40.38	Генерация: "Reagents"  1-5, 8-15, 25
475	3	5	1	0.70	2024-12-18 00:36:12.38	Генерация: "Reagents"  1-5, 8-15, 25
476	3	7	4	81.28	2024-04-15 18:22:20.38	Генерация: "Reagents"  1-5, 8-15, 25
477	3	8	2	63.27	2026-04-05 02:53:21.38	Генерация: "Reagents"  1-5, 8-15, 25
478	3	6	4	29.78	2024-10-18 07:53:30.38	Генерация: "Reagents"  1-5, 8-15, 25
479	3	5	3	29.26	2024-05-14 09:45:30.38	Генерация: "Reagents"  1-5, 8-15, 25
480	3	6	4	37.76	2024-07-18 20:14:36.38	Генерация: "Reagents"  1-5, 8-15, 25
481	3	8	2	-42.24	2026-03-28 01:03:23.38	Генерация: "Reagents"  1-5, 8-15, 25
482	3	5	4	42.64	2025-05-17 19:29:30.38	Генерация: "Reagents"  1-5, 8-15, 25
483	3	8	3	37.88	2025-05-01 21:26:26.38	Генерация: "Reagents"  1-5, 8-15, 25
484	3	7	3	17.56	2026-03-28 16:40:24.38	Генерация: "Reagents"  1-5, 8-15, 25
485	3	7	4	73.25	2025-02-08 12:46:03.38	Генерация: "Reagents"  1-5, 8-15, 25
486	3	7	4	-29.25	2025-06-21 20:38:41.38	Генерация: "Reagents"  1-5, 8-15, 25
487	3	6	4	-8.40	2024-04-26 21:12:00.38	Генерация: "Reagents"  1-5, 8-15, 25
488	3	6	1	63.85	2025-09-01 12:06:07.38	Генерация: "Reagents"  1-5, 8-15, 25
489	3	8	1	30.88	2025-02-25 12:48:27.38	Генерация: "Reagents"  1-5, 8-15, 25
490	3	8	2	-5.33	2024-05-09 09:31:44.38	Генерация: "Reagents"  1-5, 8-15, 25
491	3	7	3	-13.15	2024-11-18 01:48:53.38	Генерация: "Reagents"  1-5, 8-15, 25
492	3	6	3	-32.60	2024-08-30 18:36:21.38	Генерация: "Reagents"  1-5, 8-15, 25
493	3	6	5	82.34	2024-09-04 22:19:51.38	Генерация: "Reagents"  1-5, 8-15, 25
494	3	7	1	76.88	2026-01-02 11:16:52.38	Генерация: "Reagents"  1-5, 8-15, 25
495	3	6	1	1.48	2024-04-12 03:17:36.38	Генерация: "Reagents"  1-5, 8-15, 25
496	3	8	3	76.62	2024-05-08 17:43:40.38	Генерация: "Reagents"  1-5, 8-15, 25
497	3	8	4	-20.74	2025-06-12 11:07:14.38	Генерация: "Reagents"  1-5, 8-15, 25
498	3	8	4	7.81	2025-01-25 05:56:11.38	Генерация: "Reagents"  1-5, 8-15, 25
499	3	6	3	-28.07	2024-10-29 15:10:37.38	Генерация: "Reagents"  1-5, 8-15, 25
500	3	8	1	23.31	2025-04-10 10:59:43.38	Генерация: "Reagents"  1-5, 8-15, 25
756	4	7	2	-33.01	2025-02-24 16:19:38.11	Генерация: "Reagents"  1-5, 8-15, 25
757	4	7	2	9.80	2024-06-07 02:24:03.11	Генерация: "Reagents"  1-5, 8-15, 25
758	4	6	1	66.56	2024-12-28 02:37:25.11	Генерация: "Reagents"  1-5, 8-15, 25
759	4	5	2	-44.50	2025-05-31 22:54:50.11	Генерация: "Reagents"  1-5, 8-15, 25
760	4	7	5	-29.02	2024-11-02 14:23:10.11	Генерация: "Reagents"  1-5, 8-15, 25
761	4	7	4	-39.06	2025-03-18 07:13:40.11	Генерация: "Reagents"  1-5, 8-15, 25
762	4	5	5	80.23	2024-12-26 22:03:04.11	Генерация: "Reagents"  1-5, 8-15, 25
763	4	5	1	39.83	2025-10-05 07:04:30.11	Генерация: "Reagents"  1-5, 8-15, 25
764	4	5	1	64.07	2025-08-04 12:23:33.11	Генерация: "Reagents"  1-5, 8-15, 25
765	4	7	2	30.22	2026-01-08 09:09:48.11	Генерация: "Reagents"  1-5, 8-15, 25
766	4	8	5	-4.86	2024-04-23 03:57:39.11	Генерация: "Reagents"  1-5, 8-15, 25
767	4	5	4	68.23	2024-04-14 12:39:14.11	Генерация: "Reagents"  1-5, 8-15, 25
768	4	5	5	71.51	2024-07-04 02:05:10.11	Генерация: "Reagents"  1-5, 8-15, 25
769	4	7	1	7.14	2026-02-06 01:34:01.11	Генерация: "Reagents"  1-5, 8-15, 25
770	4	6	4	98.18	2026-03-23 00:21:32.11	Генерация: "Reagents"  1-5, 8-15, 25
771	4	8	5	68.18	2024-04-07 18:26:45.11	Генерация: "Reagents"  1-5, 8-15, 25
772	4	8	5	56.00	2024-12-26 02:25:40.11	Генерация: "Reagents"  1-5, 8-15, 25
773	4	8	5	-6.51	2025-06-11 01:42:30.11	Генерация: "Reagents"  1-5, 8-15, 25
774	4	6	5	-26.13	2024-04-18 17:03:49.11	Генерация: "Reagents"  1-5, 8-15, 25
775	4	6	3	-5.26	2025-07-05 10:58:18.11	Генерация: "Reagents"  1-5, 8-15, 25
776	4	8	3	42.84	2024-05-31 07:12:19.11	Генерация: "Reagents"  1-5, 8-15, 25
777	4	7	2	31.10	2025-05-01 15:00:44.11	Генерация: "Reagents"  1-5, 8-15, 25
778	4	5	5	97.81	2025-11-08 22:59:58.11	Генерация: "Reagents"  1-5, 8-15, 25
779	4	6	4	-2.82	2026-02-06 18:54:20.11	Генерация: "Reagents"  1-5, 8-15, 25
780	4	7	4	43.77	2024-05-14 20:51:50.11	Генерация: "Reagents"  1-5, 8-15, 25
781	4	8	4	-29.63	2024-10-15 17:28:54.11	Генерация: "Reagents"  1-5, 8-15, 25
782	4	5	2	-48.99	2025-10-13 20:16:18.11	Генерация: "Reagents"  1-5, 8-15, 25
783	4	5	3	3.22	2024-05-25 22:48:23.11	Генерация: "Reagents"  1-5, 8-15, 25
784	4	8	5	84.18	2025-04-28 06:16:14.11	Генерация: "Reagents"  1-5, 8-15, 25
785	4	7	5	51.24	2025-12-06 21:27:31.11	Генерация: "Reagents"  1-5, 8-15, 25
786	4	7	2	96.19	2024-05-31 12:15:23.11	Генерация: "Reagents"  1-5, 8-15, 25
787	4	6	3	11.73	2025-07-11 21:34:31.11	Генерация: "Reagents"  1-5, 8-15, 25
788	4	6	2	-13.33	2025-08-10 15:54:42.11	Генерация: "Reagents"  1-5, 8-15, 25
789	4	8	5	41.62	2024-04-28 18:06:46.11	Генерация: "Reagents"  1-5, 8-15, 25
790	4	8	5	13.15	2024-04-16 19:07:42.11	Генерация: "Reagents"  1-5, 8-15, 25
791	4	6	5	20.66	2024-07-29 09:59:05.11	Генерация: "Reagents"  1-5, 8-15, 25
792	4	8	5	76.98	2025-05-08 20:15:24.11	Генерация: "Reagents"  1-5, 8-15, 25
793	4	7	2	-40.34	2025-12-01 05:33:25.11	Генерация: "Reagents"  1-5, 8-15, 25
794	4	8	2	-7.24	2024-06-24 10:24:16.11	Генерация: "Reagents"  1-5, 8-15, 25
795	4	8	2	-42.92	2024-12-28 16:42:19.11	Генерация: "Reagents"  1-5, 8-15, 25
796	4	6	4	50.70	2025-11-26 09:26:04.11	Генерация: "Reagents"  1-5, 8-15, 25
797	4	5	3	43.85	2024-12-04 01:17:56.11	Генерация: "Reagents"  1-5, 8-15, 25
798	4	5	3	38.70	2024-09-28 14:34:17.11	Генерация: "Reagents"  1-5, 8-15, 25
799	4	6	5	81.01	2024-05-02 08:15:44.11	Генерация: "Reagents"  1-5, 8-15, 25
800	4	8	3	32.76	2024-11-17 03:00:10.11	Генерация: "Reagents"  1-5, 8-15, 25
801	4	5	4	77.55	2026-04-03 05:32:00.11	Генерация: "Reagents"  1-5, 8-15, 25
802	4	6	3	14.74	2026-01-29 21:42:00.11	Генерация: "Reagents"  1-5, 8-15, 25
803	4	8	3	80.01	2025-03-17 01:42:04.11	Генерация: "Reagents"  1-5, 8-15, 25
804	4	6	2	62.73	2024-12-09 03:27:44.11	Генерация: "Reagents"  1-5, 8-15, 25
805	4	7	5	-2.64	2024-12-04 16:06:59.11	Генерация: "Reagents"  1-5, 8-15, 25
806	4	7	1	40.80	2025-11-12 11:13:09.11	Генерация: "Reagents"  1-5, 8-15, 25
807	4	5	3	-35.10	2024-08-23 09:29:41.11	Генерация: "Reagents"  1-5, 8-15, 25
808	4	7	2	32.52	2026-03-26 00:54:20.11	Генерация: "Reagents"  1-5, 8-15, 25
809	4	7	2	-23.80	2025-09-27 16:04:05.11	Генерация: "Reagents"  1-5, 8-15, 25
810	4	7	5	77.55	2024-08-21 20:28:40.11	Генерация: "Reagents"  1-5, 8-15, 25
811	4	8	5	-43.88	2025-08-28 18:10:08.11	Генерация: "Reagents"  1-5, 8-15, 25
812	4	5	5	55.99	2024-08-17 03:49:35.11	Генерация: "Reagents"  1-5, 8-15, 25
813	4	5	1	45.97	2025-11-03 14:07:49.11	Генерация: "Reagents"  1-5, 8-15, 25
814	4	5	5	40.64	2024-04-22 19:47:25.11	Генерация: "Reagents"  1-5, 8-15, 25
815	4	5	3	79.63	2025-10-14 02:42:20.11	Генерация: "Reagents"  1-5, 8-15, 25
816	4	5	5	-22.09	2024-06-12 18:18:55.11	Генерация: "Reagents"  1-5, 8-15, 25
817	4	7	4	-12.35	2024-11-09 22:51:57.11	Генерация: "Reagents"  1-5, 8-15, 25
818	4	7	5	13.68	2024-08-21 11:59:11.11	Генерация: "Reagents"  1-5, 8-15, 25
819	4	5	4	43.24	2024-10-08 01:24:01.11	Генерация: "Reagents"  1-5, 8-15, 25
820	4	8	3	17.63	2025-04-03 21:53:51.11	Генерация: "Reagents"  1-5, 8-15, 25
821	4	8	1	10.57	2025-12-17 06:27:10.11	Генерация: "Reagents"  1-5, 8-15, 25
822	4	8	1	39.06	2024-10-01 10:42:03.11	Генерация: "Reagents"  1-5, 8-15, 25
823	4	7	2	-22.78	2026-03-15 16:11:32.11	Генерация: "Reagents"  1-5, 8-15, 25
824	4	8	1	32.51	2024-06-24 18:15:18.11	Генерация: "Reagents"  1-5, 8-15, 25
825	4	6	3	-7.21	2024-06-11 05:53:53.11	Генерация: "Reagents"  1-5, 8-15, 25
826	4	7	1	47.19	2024-06-28 07:34:24.11	Генерация: "Reagents"  1-5, 8-15, 25
827	4	5	3	-5.09	2025-08-08 13:52:06.11	Генерация: "Reagents"  1-5, 8-15, 25
828	4	6	2	-31.73	2025-12-20 08:00:08.11	Генерация: "Reagents"  1-5, 8-15, 25
829	4	7	3	51.02	2024-06-18 01:08:25.11	Генерация: "Reagents"  1-5, 8-15, 25
830	4	7	2	-8.65	2025-02-05 16:32:13.11	Генерация: "Reagents"  1-5, 8-15, 25
831	4	5	5	-13.71	2025-09-11 12:49:16.11	Генерация: "Reagents"  1-5, 8-15, 25
832	4	8	2	66.14	2025-05-01 01:26:48.11	Генерация: "Reagents"  1-5, 8-15, 25
833	4	7	2	-46.78	2026-01-01 02:25:51.11	Генерация: "Reagents"  1-5, 8-15, 25
834	4	5	2	34.70	2025-01-20 20:49:47.11	Генерация: "Reagents"  1-5, 8-15, 25
835	4	8	5	46.44	2025-12-05 12:14:44.11	Генерация: "Reagents"  1-5, 8-15, 25
836	4	6	4	-39.11	2026-01-19 11:41:07.11	Генерация: "Reagents"  1-5, 8-15, 25
837	4	8	1	19.23	2025-07-01 14:08:22.11	Генерация: "Reagents"  1-5, 8-15, 25
838	4	8	5	-29.07	2025-01-23 07:19:41.11	Генерация: "Reagents"  1-5, 8-15, 25
839	4	6	3	46.22	2024-04-21 23:03:21.11	Генерация: "Reagents"  1-5, 8-15, 25
840	4	7	5	-20.22	2025-08-29 19:23:45.11	Генерация: "Reagents"  1-5, 8-15, 25
841	4	6	4	52.36	2025-05-08 05:31:43.11	Генерация: "Reagents"  1-5, 8-15, 25
842	4	8	3	51.71	2024-08-23 06:32:26.11	Генерация: "Reagents"  1-5, 8-15, 25
843	4	6	2	47.62	2024-06-04 10:33:20.11	Генерация: "Reagents"  1-5, 8-15, 25
844	4	7	1	96.95	2025-05-23 12:37:50.11	Генерация: "Reagents"  1-5, 8-15, 25
845	4	8	5	-21.02	2024-11-27 11:35:17.11	Генерация: "Reagents"  1-5, 8-15, 25
846	4	8	2	-32.09	2025-07-13 18:50:39.11	Генерация: "Reagents"  1-5, 8-15, 25
847	4	7	2	-10.19	2025-06-19 16:36:49.11	Генерация: "Reagents"  1-5, 8-15, 25
848	4	6	5	-1.67	2026-03-29 14:39:10.11	Генерация: "Reagents"  1-5, 8-15, 25
849	4	6	2	45.10	2024-09-08 16:31:06.11	Генерация: "Reagents"  1-5, 8-15, 25
850	4	8	5	24.38	2024-08-03 21:17:08.11	Генерация: "Reagents"  1-5, 8-15, 25
851	4	5	4	-40.19	2025-04-30 01:21:10.11	Генерация: "Reagents"  1-5, 8-15, 25
852	4	8	3	23.54	2024-07-05 20:24:24.11	Генерация: "Reagents"  1-5, 8-15, 25
853	4	6	1	31.35	2024-07-04 11:21:32.11	Генерация: "Reagents"  1-5, 8-15, 25
854	4	5	1	82.09	2024-11-07 14:46:21.11	Генерация: "Reagents"  1-5, 8-15, 25
855	4	8	5	49.79	2024-05-23 03:08:58.11	Генерация: "Reagents"  1-5, 8-15, 25
856	4	8	3	51.56	2025-01-03 03:27:55.11	Генерация: "Reagents"  1-5, 8-15, 25
857	4	5	4	63.09	2025-01-07 20:48:03.11	Генерация: "Reagents"  1-5, 8-15, 25
858	4	6	3	81.26	2025-04-30 22:18:08.11	Генерация: "Reagents"  1-5, 8-15, 25
859	4	5	5	46.46	2025-04-23 22:23:47.11	Генерация: "Reagents"  1-5, 8-15, 25
860	4	6	1	54.07	2024-10-13 09:35:14.11	Генерация: "Reagents"  1-5, 8-15, 25
861	4	5	2	44.79	2026-01-11 10:08:04.11	Генерация: "Reagents"  1-5, 8-15, 25
862	4	7	2	-29.15	2025-02-02 16:27:24.11	Генерация: "Reagents"  1-5, 8-15, 25
863	4	5	5	66.06	2026-03-30 15:23:38.11	Генерация: "Reagents"  1-5, 8-15, 25
864	4	6	3	-25.28	2025-04-01 01:44:16.11	Генерация: "Reagents"  1-5, 8-15, 25
865	4	8	4	43.00	2025-07-11 10:05:02.11	Генерация: "Reagents"  1-5, 8-15, 25
866	4	8	5	85.34	2024-08-25 09:08:41.11	Генерация: "Reagents"  1-5, 8-15, 25
867	4	8	2	-45.23	2026-01-24 10:27:10.11	Генерация: "Reagents"  1-5, 8-15, 25
868	4	5	1	0.98	2025-12-12 10:27:15.11	Генерация: "Reagents"  1-5, 8-15, 25
869	4	5	4	87.79	2024-10-02 00:54:11.11	Генерация: "Reagents"  1-5, 8-15, 25
870	4	5	5	-45.14	2026-01-30 18:45:14.11	Генерация: "Reagents"  1-5, 8-15, 25
871	4	7	4	71.59	2025-11-04 20:00:34.11	Генерация: "Reagents"  1-5, 8-15, 25
872	4	5	4	-31.78	2025-03-20 12:52:15.11	Генерация: "Reagents"  1-5, 8-15, 25
873	4	5	5	8.26	2025-12-08 23:52:09.11	Генерация: "Reagents"  1-5, 8-15, 25
874	4	6	1	78.88	2024-06-15 22:21:07.11	Генерация: "Reagents"  1-5, 8-15, 25
875	4	5	4	-44.30	2024-06-21 06:28:22.11	Генерация: "Reagents"  1-5, 8-15, 25
876	4	7	3	19.25	2026-02-05 22:27:11.11	Генерация: "Reagents"  1-5, 8-15, 25
877	4	7	1	15.39	2025-08-06 21:19:09.11	Генерация: "Reagents"  1-5, 8-15, 25
878	4	7	4	5.01	2026-01-24 00:36:22.11	Генерация: "Reagents"  1-5, 8-15, 25
879	4	5	3	-20.58	2025-05-27 10:33:39.11	Генерация: "Reagents"  1-5, 8-15, 25
880	4	6	2	61.75	2025-09-15 07:01:55.11	Генерация: "Reagents"  1-5, 8-15, 25
881	4	5	5	26.32	2024-08-17 17:44:42.11	Генерация: "Reagents"  1-5, 8-15, 25
882	4	7	3	41.35	2024-04-29 16:02:29.11	Генерация: "Reagents"  1-5, 8-15, 25
883	4	8	3	34.57	2024-08-08 22:10:25.11	Генерация: "Reagents"  1-5, 8-15, 25
884	4	7	4	16.43	2024-09-07 16:02:41.11	Генерация: "Reagents"  1-5, 8-15, 25
885	4	6	2	98.25	2025-03-27 22:08:38.11	Генерация: "Reagents"  1-5, 8-15, 25
886	4	6	3	-36.72	2024-11-30 13:30:00.11	Генерация: "Reagents"  1-5, 8-15, 25
887	4	5	4	-37.72	2024-12-08 21:57:21.11	Генерация: "Reagents"  1-5, 8-15, 25
888	4	5	1	31.91	2025-07-19 07:28:57.11	Генерация: "Reagents"  1-5, 8-15, 25
889	4	8	4	3.47	2025-08-10 10:07:36.11	Генерация: "Reagents"  1-5, 8-15, 25
890	4	7	5	44.76	2026-03-06 05:09:54.11	Генерация: "Reagents"  1-5, 8-15, 25
891	4	7	3	5.64	2025-07-21 22:52:44.11	Генерация: "Reagents"  1-5, 8-15, 25
892	4	6	2	-49.26	2024-08-15 11:36:33.11	Генерация: "Reagents"  1-5, 8-15, 25
893	4	6	3	-49.78	2025-06-18 22:09:43.11	Генерация: "Reagents"  1-5, 8-15, 25
894	4	6	4	35.29	2025-03-08 17:08:35.11	Генерация: "Reagents"  1-5, 8-15, 25
895	4	5	1	96.03	2025-01-10 16:00:58.11	Генерация: "Reagents"  1-5, 8-15, 25
896	4	5	4	5.98	2024-10-06 19:45:54.11	Генерация: "Reagents"  1-5, 8-15, 25
897	4	7	5	-14.55	2025-06-25 02:59:22.11	Генерация: "Reagents"  1-5, 8-15, 25
898	4	5	1	87.15	2024-08-30 18:23:31.11	Генерация: "Reagents"  1-5, 8-15, 25
899	4	7	1	35.63	2025-12-25 22:50:14.11	Генерация: "Reagents"  1-5, 8-15, 25
900	4	5	5	-26.80	2024-05-29 08:42:36.11	Генерация: "Reagents"  1-5, 8-15, 25
901	4	8	2	58.56	2026-03-05 14:46:04.11	Генерация: "Reagents"  1-5, 8-15, 25
902	4	8	1	45.92	2024-06-28 04:21:45.11	Генерация: "Reagents"  1-5, 8-15, 25
903	4	5	2	94.27	2024-10-15 07:01:13.11	Генерация: "Reagents"  1-5, 8-15, 25
904	4	8	1	41.50	2025-01-06 17:55:16.11	Генерация: "Reagents"  1-5, 8-15, 25
905	4	5	1	11.99	2025-12-03 18:43:32.11	Генерация: "Reagents"  1-5, 8-15, 25
906	5	6	4	16.86	2025-06-19 23:00:39.89	Генерация: "Reagents"  1-5, 8-15, 25
907	5	7	5	87.98	2025-11-09 21:36:49.89	Генерация: "Reagents"  1-5, 8-15, 25
908	5	6	5	-2.38	2024-05-27 18:26:22.89	Генерация: "Reagents"  1-5, 8-15, 25
909	5	5	3	94.30	2026-02-17 19:57:01.89	Генерация: "Reagents"  1-5, 8-15, 25
910	5	7	1	3.33	2026-03-01 03:43:07.89	Генерация: "Reagents"  1-5, 8-15, 25
911	5	6	1	85.96	2025-02-23 04:24:34.89	Генерация: "Reagents"  1-5, 8-15, 25
912	5	5	3	22.66	2024-08-18 02:55:59.89	Генерация: "Reagents"  1-5, 8-15, 25
913	5	8	2	83.20	2025-04-14 21:54:50.89	Генерация: "Reagents"  1-5, 8-15, 25
914	5	8	2	68.84	2026-01-27 10:05:33.89	Генерация: "Reagents"  1-5, 8-15, 25
915	5	5	4	30.97	2024-07-21 10:32:31.89	Генерация: "Reagents"  1-5, 8-15, 25
916	5	8	2	72.31	2024-11-04 00:51:25.89	Генерация: "Reagents"  1-5, 8-15, 25
917	5	8	2	82.17	2024-10-05 05:50:26.89	Генерация: "Reagents"  1-5, 8-15, 25
918	5	7	3	57.88	2024-08-12 16:00:41.89	Генерация: "Reagents"  1-5, 8-15, 25
919	5	5	3	-49.27	2024-10-29 04:56:35.89	Генерация: "Reagents"  1-5, 8-15, 25
920	5	6	3	29.90	2026-02-10 10:46:06.89	Генерация: "Reagents"  1-5, 8-15, 25
921	5	6	4	-43.05	2025-02-05 00:40:15.89	Генерация: "Reagents"  1-5, 8-15, 25
922	5	6	3	95.56	2026-02-11 01:57:36.89	Генерация: "Reagents"  1-5, 8-15, 25
923	5	7	3	-31.55	2024-06-24 22:28:20.89	Генерация: "Reagents"  1-5, 8-15, 25
924	5	8	1	57.25	2025-09-15 14:40:41.89	Генерация: "Reagents"  1-5, 8-15, 25
925	5	8	2	-7.07	2026-02-08 08:29:21.89	Генерация: "Reagents"  1-5, 8-15, 25
926	5	7	3	-25.70	2024-12-08 15:04:45.89	Генерация: "Reagents"  1-5, 8-15, 25
927	5	8	3	-31.01	2025-04-04 01:54:01.89	Генерация: "Reagents"  1-5, 8-15, 25
928	5	6	1	39.27	2025-03-09 06:48:53.89	Генерация: "Reagents"  1-5, 8-15, 25
929	5	6	5	-37.53	2024-09-05 15:19:35.89	Генерация: "Reagents"  1-5, 8-15, 25
930	5	6	2	2.19	2024-09-03 18:11:53.89	Генерация: "Reagents"  1-5, 8-15, 25
931	5	8	2	0.97	2025-05-24 17:16:05.89	Генерация: "Reagents"  1-5, 8-15, 25
932	5	8	1	0.43	2025-03-26 01:14:53.89	Генерация: "Reagents"  1-5, 8-15, 25
933	5	5	5	-1.89	2025-08-25 02:52:04.89	Генерация: "Reagents"  1-5, 8-15, 25
934	5	5	2	89.92	2025-02-19 20:23:43.89	Генерация: "Reagents"  1-5, 8-15, 25
935	5	6	4	-24.85	2025-03-15 18:20:22.89	Генерация: "Reagents"  1-5, 8-15, 25
936	5	7	2	81.51	2025-07-05 10:48:47.89	Генерация: "Reagents"  1-5, 8-15, 25
937	5	6	4	-35.73	2025-06-29 21:37:20.89	Генерация: "Reagents"  1-5, 8-15, 25
938	5	5	5	-9.90	2024-12-02 18:58:40.89	Генерация: "Reagents"  1-5, 8-15, 25
939	5	7	4	0.13	2026-03-17 22:02:25.89	Генерация: "Reagents"  1-5, 8-15, 25
940	5	7	4	-25.86	2024-09-16 15:28:38.89	Генерация: "Reagents"  1-5, 8-15, 25
941	5	6	1	31.43	2025-07-25 15:17:09.89	Генерация: "Reagents"  1-5, 8-15, 25
942	5	5	5	-10.40	2026-01-30 02:36:54.89	Генерация: "Reagents"  1-5, 8-15, 25
943	5	5	3	5.11	2025-09-12 21:33:15.89	Генерация: "Reagents"  1-5, 8-15, 25
944	5	7	5	-15.02	2025-12-25 05:46:21.89	Генерация: "Reagents"  1-5, 8-15, 25
945	5	6	4	13.71	2025-07-13 00:34:38.89	Генерация: "Reagents"  1-5, 8-15, 25
946	5	6	4	-37.36	2025-12-05 11:29:34.89	Генерация: "Reagents"  1-5, 8-15, 25
947	5	6	4	29.62	2026-01-05 19:07:59.89	Генерация: "Reagents"  1-5, 8-15, 25
948	5	5	4	4.84	2026-02-13 18:10:09.89	Генерация: "Reagents"  1-5, 8-15, 25
949	5	8	1	43.48	2025-09-18 16:42:33.89	Генерация: "Reagents"  1-5, 8-15, 25
950	5	5	2	49.02	2025-06-04 03:02:45.89	Генерация: "Reagents"  1-5, 8-15, 25
951	5	7	4	30.63	2024-05-31 19:36:24.89	Генерация: "Reagents"  1-5, 8-15, 25
952	5	8	5	-44.14	2025-04-17 11:20:04.89	Генерация: "Reagents"  1-5, 8-15, 25
953	5	6	3	93.43	2025-06-25 01:36:35.89	Генерация: "Reagents"  1-5, 8-15, 25
954	5	6	2	47.90	2025-09-07 04:11:55.89	Генерация: "Reagents"  1-5, 8-15, 25
955	5	7	2	70.27	2024-12-06 13:06:03.89	Генерация: "Reagents"  1-5, 8-15, 25
956	5	8	4	-16.99	2024-10-14 21:21:07.89	Генерация: "Reagents"  1-5, 8-15, 25
957	5	5	2	99.72	2025-10-01 10:46:36.89	Генерация: "Reagents"  1-5, 8-15, 25
958	5	5	5	59.48	2025-11-22 15:32:57.89	Генерация: "Reagents"  1-5, 8-15, 25
959	5	6	2	31.95	2025-10-30 05:54:05.89	Генерация: "Reagents"  1-5, 8-15, 25
960	5	6	4	63.97	2025-11-23 10:50:19.89	Генерация: "Reagents"  1-5, 8-15, 25
961	5	8	3	88.95	2024-12-12 11:22:40.89	Генерация: "Reagents"  1-5, 8-15, 25
962	5	5	5	47.80	2025-05-16 02:39:53.89	Генерация: "Reagents"  1-5, 8-15, 25
963	5	6	3	66.16	2024-11-05 16:19:51.89	Генерация: "Reagents"  1-5, 8-15, 25
964	5	8	1	69.35	2024-06-28 09:04:09.89	Генерация: "Reagents"  1-5, 8-15, 25
965	5	8	5	-42.83	2024-05-04 09:18:01.89	Генерация: "Reagents"  1-5, 8-15, 25
966	5	8	4	38.16	2024-05-06 04:11:11.89	Генерация: "Reagents"  1-5, 8-15, 25
967	5	8	2	57.13	2024-09-11 10:44:31.89	Генерация: "Reagents"  1-5, 8-15, 25
968	5	5	2	36.96	2026-02-03 05:07:52.89	Генерация: "Reagents"  1-5, 8-15, 25
969	5	8	5	49.22	2025-01-10 20:04:18.89	Генерация: "Reagents"  1-5, 8-15, 25
970	5	6	3	-43.05	2025-01-19 02:09:27.89	Генерация: "Reagents"  1-5, 8-15, 25
971	5	6	4	18.10	2025-09-28 13:52:40.89	Генерация: "Reagents"  1-5, 8-15, 25
972	5	5	2	9.61	2025-09-03 17:57:05.89	Генерация: "Reagents"  1-5, 8-15, 25
973	5	5	3	-38.91	2024-10-09 14:10:14.89	Генерация: "Reagents"  1-5, 8-15, 25
974	5	7	4	59.95	2024-08-19 11:33:17.89	Генерация: "Reagents"  1-5, 8-15, 25
975	5	8	3	-43.81	2025-06-15 12:09:35.89	Генерация: "Reagents"  1-5, 8-15, 25
976	5	5	4	98.37	2024-07-20 08:42:11.89	Генерация: "Reagents"  1-5, 8-15, 25
977	5	7	3	68.95	2024-11-08 19:21:45.89	Генерация: "Reagents"  1-5, 8-15, 25
978	5	6	1	64.06	2024-05-20 13:33:22.89	Генерация: "Reagents"  1-5, 8-15, 25
979	5	7	2	-19.36	2024-08-07 16:16:45.89	Генерация: "Reagents"  1-5, 8-15, 25
980	5	6	3	80.39	2026-01-29 05:50:27.89	Генерация: "Reagents"  1-5, 8-15, 25
981	5	5	5	31.39	2025-08-03 23:02:17.89	Генерация: "Reagents"  1-5, 8-15, 25
982	5	6	5	78.74	2024-09-24 02:16:26.89	Генерация: "Reagents"  1-5, 8-15, 25
983	5	6	2	-37.91	2025-10-08 06:57:26.89	Генерация: "Reagents"  1-5, 8-15, 25
984	5	8	4	16.06	2025-02-04 00:51:03.89	Генерация: "Reagents"  1-5, 8-15, 25
985	5	5	2	-23.32	2024-05-25 01:10:01.89	Генерация: "Reagents"  1-5, 8-15, 25
986	5	8	3	46.37	2025-04-26 19:32:33.89	Генерация: "Reagents"  1-5, 8-15, 25
987	5	8	2	79.07	2024-06-07 19:05:00.89	Генерация: "Reagents"  1-5, 8-15, 25
988	5	7	5	92.97	2024-11-22 00:24:04.89	Генерация: "Reagents"  1-5, 8-15, 25
989	5	6	4	70.15	2024-10-27 05:22:33.89	Генерация: "Reagents"  1-5, 8-15, 25
990	5	6	4	78.22	2024-11-14 18:09:56.89	Генерация: "Reagents"  1-5, 8-15, 25
991	5	8	5	51.50	2024-06-18 10:04:49.89	Генерация: "Reagents"  1-5, 8-15, 25
992	5	5	1	46.67	2024-11-26 22:47:39.89	Генерация: "Reagents"  1-5, 8-15, 25
993	5	7	4	35.10	2024-09-30 02:06:44.89	Генерация: "Reagents"  1-5, 8-15, 25
994	5	7	1	10.46	2025-08-04 18:43:51.89	Генерация: "Reagents"  1-5, 8-15, 25
995	5	7	3	38.89	2025-08-08 19:38:01.89	Генерация: "Reagents"  1-5, 8-15, 25
996	5	6	1	38.87	2026-01-17 06:12:26.89	Генерация: "Reagents"  1-5, 8-15, 25
997	5	8	4	-33.67	2025-09-13 17:50:00.89	Генерация: "Reagents"  1-5, 8-15, 25
998	5	5	4	-6.48	2025-12-28 03:06:57.89	Генерация: "Reagents"  1-5, 8-15, 25
999	5	5	4	-7.87	2026-04-01 14:43:30.89	Генерация: "Reagents"  1-5, 8-15, 25
1000	5	5	4	47.58	2026-03-14 09:49:13.89	Генерация: "Reagents"  1-5, 8-15, 25
1001	5	6	2	82.03	2024-08-30 14:11:28.89	Генерация: "Reagents"  1-5, 8-15, 25
1002	5	8	3	-3.79	2024-12-19 23:58:09.89	Генерация: "Reagents"  1-5, 8-15, 25
1003	5	7	3	-43.45	2024-09-18 00:17:25.89	Генерация: "Reagents"  1-5, 8-15, 25
1004	5	5	5	-44.17	2024-06-19 03:51:54.89	Генерация: "Reagents"  1-5, 8-15, 25
1005	5	7	5	-30.64	2025-03-04 04:51:10.89	Генерация: "Reagents"  1-5, 8-15, 25
1006	5	8	1	45.92	2024-11-25 11:00:15.89	Генерация: "Reagents"  1-5, 8-15, 25
1007	5	7	3	90.29	2026-03-27 16:03:49.89	Генерация: "Reagents"  1-5, 8-15, 25
1008	5	6	1	34.39	2025-11-24 01:31:18.89	Генерация: "Reagents"  1-5, 8-15, 25
1009	5	7	3	-5.07	2025-08-25 12:55:29.89	Генерация: "Reagents"  1-5, 8-15, 25
1010	5	8	4	12.43	2024-11-02 21:38:39.89	Генерация: "Reagents"  1-5, 8-15, 25
1011	5	6	1	71.65	2024-07-27 05:53:15.89	Генерация: "Reagents"  1-5, 8-15, 25
1012	5	8	1	68.76	2025-06-20 00:44:10.89	Генерация: "Reagents"  1-5, 8-15, 25
1013	5	7	2	13.49	2025-04-23 06:29:37.89	Генерация: "Reagents"  1-5, 8-15, 25
1014	5	6	1	47.17	2026-01-21 05:40:15.89	Генерация: "Reagents"  1-5, 8-15, 25
1015	5	6	3	12.28	2024-04-19 13:35:04.89	Генерация: "Reagents"  1-5, 8-15, 25
1016	5	8	5	-33.93	2025-01-13 23:04:23.89	Генерация: "Reagents"  1-5, 8-15, 25
1017	5	6	2	48.03	2025-12-02 12:21:25.89	Генерация: "Reagents"  1-5, 8-15, 25
1018	5	6	1	95.16	2024-09-28 00:45:15.89	Генерация: "Reagents"  1-5, 8-15, 25
1019	5	6	5	22.23	2026-02-09 06:38:02.89	Генерация: "Reagents"  1-5, 8-15, 25
1020	5	6	3	-8.66	2025-08-25 04:56:27.89	Генерация: "Reagents"  1-5, 8-15, 25
1021	5	8	1	26.51	2025-07-15 03:41:32.89	Генерация: "Reagents"  1-5, 8-15, 25
1022	5	7	3	97.81	2025-09-05 12:34:08.89	Генерация: "Reagents"  1-5, 8-15, 25
1023	5	6	2	72.06	2025-10-31 04:45:58.89	Генерация: "Reagents"  1-5, 8-15, 25
1024	5	7	4	-38.68	2025-09-29 05:34:41.89	Генерация: "Reagents"  1-5, 8-15, 25
1025	5	8	2	11.58	2025-03-01 07:16:57.89	Генерация: "Reagents"  1-5, 8-15, 25
1026	5	8	1	44.83	2024-04-16 15:15:21.89	Генерация: "Reagents"  1-5, 8-15, 25
1027	5	8	3	36.15	2025-04-23 11:27:33.89	Генерация: "Reagents"  1-5, 8-15, 25
1028	5	5	2	68.31	2025-10-29 00:35:52.89	Генерация: "Reagents"  1-5, 8-15, 25
1029	5	7	3	-5.50	2025-09-04 17:25:55.89	Генерация: "Reagents"  1-5, 8-15, 25
1030	5	5	3	75.89	2025-12-09 12:12:58.89	Генерация: "Reagents"  1-5, 8-15, 25
1031	5	7	2	94.47	2024-08-29 07:47:27.89	Генерация: "Reagents"  1-5, 8-15, 25
1032	5	5	5	-13.76	2025-03-13 15:33:46.89	Генерация: "Reagents"  1-5, 8-15, 25
1033	5	8	1	85.15	2024-07-01 23:02:43.89	Генерация: "Reagents"  1-5, 8-15, 25
1034	5	8	1	6.30	2024-05-30 09:33:43.89	Генерация: "Reagents"  1-5, 8-15, 25
1035	5	7	2	58.83	2024-08-07 02:03:27.89	Генерация: "Reagents"  1-5, 8-15, 25
1036	5	8	5	18.83	2026-01-17 12:29:07.89	Генерация: "Reagents"  1-5, 8-15, 25
1037	5	5	3	79.31	2024-09-14 09:08:07.89	Генерация: "Reagents"  1-5, 8-15, 25
1038	5	8	5	55.18	2024-06-19 03:00:04.89	Генерация: "Reagents"  1-5, 8-15, 25
1039	5	5	3	-46.89	2024-04-27 06:45:26.89	Генерация: "Reagents"  1-5, 8-15, 25
1040	5	5	2	36.37	2025-11-23 08:02:29.89	Генерация: "Reagents"  1-5, 8-15, 25
1041	5	8	5	-41.48	2025-03-03 02:18:14.89	Генерация: "Reagents"  1-5, 8-15, 25
1042	5	8	4	65.03	2025-12-25 12:20:10.89	Генерация: "Reagents"  1-5, 8-15, 25
1043	5	8	3	85.38	2025-09-19 17:45:07.89	Генерация: "Reagents"  1-5, 8-15, 25
1044	5	5	3	92.61	2026-03-22 07:26:02.89	Генерация: "Reagents"  1-5, 8-15, 25
1045	5	7	3	55.87	2024-05-05 14:28:01.89	Генерация: "Reagents"  1-5, 8-15, 25
1046	5	7	1	7.21	2025-04-19 13:31:01.89	Генерация: "Reagents"  1-5, 8-15, 25
1047	5	7	3	64.34	2025-06-24 05:14:54.89	Генерация: "Reagents"  1-5, 8-15, 25
1048	5	8	1	28.52	2024-08-08 05:37:23.89	Генерация: "Reagents"  1-5, 8-15, 25
1049	5	8	2	-27.93	2025-09-26 15:07:52.89	Генерация: "Reagents"  1-5, 8-15, 25
1050	5	5	2	-6.72	2026-03-21 14:41:33.89	Генерация: "Reagents"  1-5, 8-15, 25
1051	5	7	3	6.88	2024-05-04 06:34:09.89	Генерация: "Reagents"  1-5, 8-15, 25
1052	5	6	5	98.19	2025-10-28 04:15:02.89	Генерация: "Reagents"  1-5, 8-15, 25
1053	5	5	4	26.07	2026-02-13 00:28:54.89	Генерация: "Reagents"  1-5, 8-15, 25
1054	5	6	5	61.84	2025-05-19 13:48:32.89	Генерация: "Reagents"  1-5, 8-15, 25
1055	5	8	3	86.43	2025-07-13 05:31:58.89	Генерация: "Reagents"  1-5, 8-15, 25
1056	25	5	3	79.45	2025-11-08 02:52:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1057	25	6	5	-21.03	2024-12-25 07:49:08.74	Генерация: "Reagents"  1-5, 8-15, 25
1058	25	5	1	59.65	2026-03-21 13:13:30.74	Генерация: "Reagents"  1-5, 8-15, 25
1059	25	7	1	21.53	2025-10-06 17:40:29.74	Генерация: "Reagents"  1-5, 8-15, 25
1060	25	5	3	-18.43	2025-03-22 07:25:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1061	25	6	5	-41.94	2025-07-12 15:54:42.74	Генерация: "Reagents"  1-5, 8-15, 25
1062	25	7	4	84.97	2025-03-21 22:21:16.74	Генерация: "Reagents"  1-5, 8-15, 25
1063	25	7	1	95.92	2025-07-10 07:38:12.74	Генерация: "Reagents"  1-5, 8-15, 25
1064	25	8	3	56.98	2024-11-01 17:41:38.74	Генерация: "Reagents"  1-5, 8-15, 25
1065	25	6	2	15.48	2024-07-31 12:53:57.74	Генерация: "Reagents"  1-5, 8-15, 25
1066	25	8	2	-4.96	2025-12-23 18:54:53.74	Генерация: "Reagents"  1-5, 8-15, 25
1067	25	6	1	45.82	2025-05-03 05:08:16.74	Генерация: "Reagents"  1-5, 8-15, 25
1068	25	6	2	61.30	2024-08-19 16:55:45.74	Генерация: "Reagents"  1-5, 8-15, 25
1069	25	7	2	52.33	2025-09-13 18:25:43.74	Генерация: "Reagents"  1-5, 8-15, 25
1070	25	8	2	57.36	2025-06-23 19:14:30.74	Генерация: "Reagents"  1-5, 8-15, 25
1071	25	7	5	62.75	2024-07-09 07:13:41.74	Генерация: "Reagents"  1-5, 8-15, 25
1072	25	7	3	76.53	2024-12-16 22:59:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1073	25	8	2	42.55	2024-08-31 16:36:53.74	Генерация: "Reagents"  1-5, 8-15, 25
1074	25	8	3	-3.04	2025-08-15 11:27:39.74	Генерация: "Reagents"  1-5, 8-15, 25
1075	25	6	3	69.21	2024-10-14 14:01:36.74	Генерация: "Reagents"  1-5, 8-15, 25
1076	25	6	1	4.79	2024-04-14 06:58:59.74	Генерация: "Reagents"  1-5, 8-15, 25
1077	25	5	4	51.02	2026-03-21 16:01:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1078	25	8	2	-49.23	2024-05-24 01:27:10.74	Генерация: "Reagents"  1-5, 8-15, 25
1079	25	5	2	25.63	2025-10-03 10:02:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1080	25	7	4	65.90	2024-11-15 18:35:24.74	Генерация: "Reagents"  1-5, 8-15, 25
1081	25	7	4	-22.55	2025-12-23 03:21:09.74	Генерация: "Reagents"  1-5, 8-15, 25
1082	25	7	2	86.54	2026-03-01 02:27:21.74	Генерация: "Reagents"  1-5, 8-15, 25
1083	25	6	1	41.10	2025-11-06 07:47:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1084	25	6	1	28.40	2025-06-30 07:48:44.74	Генерация: "Reagents"  1-5, 8-15, 25
1085	25	8	5	85.89	2024-09-11 12:10:33.74	Генерация: "Reagents"  1-5, 8-15, 25
1086	25	6	2	-45.94	2026-02-06 12:36:36.74	Генерация: "Reagents"  1-5, 8-15, 25
1087	25	6	4	34.39	2025-02-04 10:01:58.74	Генерация: "Reagents"  1-5, 8-15, 25
1088	25	6	1	38.26	2024-05-20 17:43:59.74	Генерация: "Reagents"  1-5, 8-15, 25
1089	25	5	2	29.20	2025-08-24 19:38:48.74	Генерация: "Reagents"  1-5, 8-15, 25
1090	25	7	2	39.86	2025-11-16 20:48:53.74	Генерация: "Reagents"  1-5, 8-15, 25
1091	25	8	2	-34.66	2025-04-03 09:20:24.74	Генерация: "Reagents"  1-5, 8-15, 25
1092	25	7	4	-33.84	2025-01-05 00:46:35.74	Генерация: "Reagents"  1-5, 8-15, 25
1093	25	8	3	27.72	2025-10-13 10:56:49.74	Генерация: "Reagents"  1-5, 8-15, 25
1094	25	6	2	3.32	2024-12-18 07:39:43.74	Генерация: "Reagents"  1-5, 8-15, 25
1095	25	8	5	4.46	2025-08-15 21:46:01.74	Генерация: "Reagents"  1-5, 8-15, 25
1096	25	8	3	33.50	2026-02-04 09:11:15.74	Генерация: "Reagents"  1-5, 8-15, 25
1097	25	6	5	47.58	2025-03-27 04:21:10.74	Генерация: "Reagents"  1-5, 8-15, 25
1098	25	6	3	70.70	2025-07-09 03:27:19.74	Генерация: "Reagents"  1-5, 8-15, 25
1099	25	6	4	-35.00	2024-12-18 21:41:29.74	Генерация: "Reagents"  1-5, 8-15, 25
1100	25	7	4	44.96	2024-06-01 05:33:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1101	25	7	2	60.28	2025-01-16 13:28:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1102	25	8	2	43.37	2024-05-21 10:02:08.74	Генерация: "Reagents"  1-5, 8-15, 25
1103	25	6	2	17.80	2025-09-10 13:35:24.74	Генерация: "Reagents"  1-5, 8-15, 25
1104	25	7	4	-35.06	2026-03-12 03:09:41.74	Генерация: "Reagents"  1-5, 8-15, 25
1105	25	6	1	63.11	2025-08-08 13:51:49.74	Генерация: "Reagents"  1-5, 8-15, 25
1106	25	8	3	90.13	2025-07-08 13:42:06.74	Генерация: "Reagents"  1-5, 8-15, 25
1107	25	5	2	99.55	2024-09-27 03:00:21.74	Генерация: "Reagents"  1-5, 8-15, 25
1108	25	8	2	27.22	2025-04-29 23:21:56.74	Генерация: "Reagents"  1-5, 8-15, 25
1109	25	8	5	40.24	2024-08-05 16:46:15.74	Генерация: "Reagents"  1-5, 8-15, 25
1110	25	8	5	59.18	2024-07-24 21:18:55.74	Генерация: "Reagents"  1-5, 8-15, 25
1111	25	8	5	72.76	2024-05-06 04:48:17.74	Генерация: "Reagents"  1-5, 8-15, 25
1112	25	6	4	28.74	2026-01-17 03:50:01.74	Генерация: "Reagents"  1-5, 8-15, 25
1113	25	7	5	95.72	2025-10-14 05:36:17.74	Генерация: "Reagents"  1-5, 8-15, 25
1114	25	5	4	43.74	2024-12-31 11:22:27.74	Генерация: "Reagents"  1-5, 8-15, 25
1115	25	6	2	-49.89	2024-12-05 06:08:37.74	Генерация: "Reagents"  1-5, 8-15, 25
1116	25	6	5	24.61	2024-12-09 06:42:15.74	Генерация: "Reagents"  1-5, 8-15, 25
1117	25	6	1	44.75	2025-07-29 01:38:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1118	25	6	2	45.58	2024-10-15 02:01:35.74	Генерация: "Reagents"  1-5, 8-15, 25
1119	25	7	3	30.45	2024-09-11 18:49:57.74	Генерация: "Reagents"  1-5, 8-15, 25
1120	25	7	3	25.29	2025-10-03 09:44:36.74	Генерация: "Reagents"  1-5, 8-15, 25
1121	25	8	1	6.01	2026-03-03 15:30:24.74	Генерация: "Reagents"  1-5, 8-15, 25
1122	25	5	5	59.58	2025-01-04 09:25:16.74	Генерация: "Reagents"  1-5, 8-15, 25
1123	25	5	3	25.43	2024-11-19 05:21:25.74	Генерация: "Reagents"  1-5, 8-15, 25
1124	25	8	1	22.52	2025-06-22 07:49:56.74	Генерация: "Reagents"  1-5, 8-15, 25
1125	25	7	5	77.79	2024-08-28 12:07:58.74	Генерация: "Reagents"  1-5, 8-15, 25
1126	25	5	5	53.36	2024-09-09 23:44:33.74	Генерация: "Reagents"  1-5, 8-15, 25
1127	25	8	2	-3.55	2026-03-18 11:13:59.74	Генерация: "Reagents"  1-5, 8-15, 25
1128	25	8	5	55.21	2024-07-04 08:05:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1129	25	7	4	-30.07	2024-06-20 22:31:17.74	Генерация: "Reagents"  1-5, 8-15, 25
1130	25	8	1	15.79	2024-11-05 19:15:59.74	Генерация: "Reagents"  1-5, 8-15, 25
1131	25	5	2	40.78	2025-07-07 09:07:49.74	Генерация: "Reagents"  1-5, 8-15, 25
1132	25	6	2	-46.23	2025-01-18 04:46:06.74	Генерация: "Reagents"  1-5, 8-15, 25
1133	25	7	1	15.33	2025-04-20 05:06:12.74	Генерация: "Reagents"  1-5, 8-15, 25
1134	25	6	2	44.57	2024-07-17 09:25:20.74	Генерация: "Reagents"  1-5, 8-15, 25
1135	25	6	5	-37.21	2024-08-02 22:12:09.74	Генерация: "Reagents"  1-5, 8-15, 25
1136	25	7	4	26.85	2025-12-17 02:24:20.74	Генерация: "Reagents"  1-5, 8-15, 25
1137	25	8	1	12.26	2026-02-19 20:47:53.74	Генерация: "Reagents"  1-5, 8-15, 25
1138	25	7	2	43.44	2026-03-06 21:44:07.74	Генерация: "Reagents"  1-5, 8-15, 25
1139	25	6	4	-25.96	2025-07-28 10:29:40.74	Генерация: "Reagents"  1-5, 8-15, 25
1140	25	6	2	-46.31	2025-12-10 09:08:58.74	Генерация: "Reagents"  1-5, 8-15, 25
1141	25	6	3	-36.61	2025-03-15 06:38:44.74	Генерация: "Reagents"  1-5, 8-15, 25
1142	25	7	2	0.36	2025-08-23 15:03:08.74	Генерация: "Reagents"  1-5, 8-15, 25
1143	25	5	5	-17.71	2024-08-28 03:28:27.74	Генерация: "Reagents"  1-5, 8-15, 25
1144	25	5	5	7.53	2024-05-15 11:41:27.74	Генерация: "Reagents"  1-5, 8-15, 25
1145	25	7	5	60.02	2026-01-09 02:01:43.74	Генерация: "Reagents"  1-5, 8-15, 25
1146	25	6	4	22.04	2025-12-04 04:55:52.74	Генерация: "Reagents"  1-5, 8-15, 25
1147	25	7	4	97.03	2024-04-24 03:33:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1148	25	8	4	-6.35	2025-12-02 08:11:40.74	Генерация: "Reagents"  1-5, 8-15, 25
1149	25	6	4	41.85	2025-04-29 22:22:25.74	Генерация: "Reagents"  1-5, 8-15, 25
1150	25	6	3	53.18	2025-05-30 15:57:28.74	Генерация: "Reagents"  1-5, 8-15, 25
1151	25	7	3	40.28	2024-05-15 05:58:26.74	Генерация: "Reagents"  1-5, 8-15, 25
1152	25	8	1	78.74	2024-09-28 21:12:42.74	Генерация: "Reagents"  1-5, 8-15, 25
1153	25	8	1	45.34	2024-08-26 00:03:57.74	Генерация: "Reagents"  1-5, 8-15, 25
1154	25	6	1	32.83	2024-07-30 02:31:12.74	Генерация: "Reagents"  1-5, 8-15, 25
1155	25	7	5	54.12	2025-04-07 20:57:03.74	Генерация: "Reagents"  1-5, 8-15, 25
1156	25	5	2	-36.84	2025-06-30 21:40:37.74	Генерация: "Reagents"  1-5, 8-15, 25
1157	25	5	5	2.56	2025-10-07 12:54:00.74	Генерация: "Reagents"  1-5, 8-15, 25
1158	25	7	2	-42.56	2026-04-07 02:21:02.74	Генерация: "Reagents"  1-5, 8-15, 25
1159	25	6	4	82.69	2025-09-10 11:43:35.74	Генерация: "Reagents"  1-5, 8-15, 25
1160	25	7	3	-42.85	2025-07-16 08:19:40.74	Генерация: "Reagents"  1-5, 8-15, 25
1161	25	8	3	14.41	2025-10-16 00:20:35.74	Генерация: "Reagents"  1-5, 8-15, 25
1162	25	8	3	-5.56	2024-11-09 15:43:03.74	Генерация: "Reagents"  1-5, 8-15, 25
1163	25	7	2	96.65	2024-08-08 11:27:05.74	Генерация: "Reagents"  1-5, 8-15, 25
1164	25	5	4	-21.62	2025-02-21 11:52:30.74	Генерация: "Reagents"  1-5, 8-15, 25
1165	25	6	3	-20.01	2024-06-25 11:33:58.74	Генерация: "Reagents"  1-5, 8-15, 25
1166	25	5	3	6.12	2025-02-17 04:42:15.74	Генерация: "Reagents"  1-5, 8-15, 25
1167	25	6	4	78.28	2024-12-29 17:54:56.74	Генерация: "Reagents"  1-5, 8-15, 25
1168	25	7	4	-14.81	2025-12-10 20:54:09.74	Генерация: "Reagents"  1-5, 8-15, 25
1169	25	8	3	-33.98	2025-01-25 23:34:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1170	25	7	1	64.32	2024-10-01 21:54:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1171	25	5	5	88.79	2024-05-04 05:10:16.74	Генерация: "Reagents"  1-5, 8-15, 25
1172	25	8	5	81.62	2025-12-27 10:53:20.74	Генерация: "Reagents"  1-5, 8-15, 25
1173	25	5	5	-15.78	2024-06-08 09:55:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1174	25	8	3	-44.73	2024-09-10 08:38:02.74	Генерация: "Reagents"  1-5, 8-15, 25
1175	25	6	4	-34.02	2024-04-29 18:43:48.74	Генерация: "Reagents"  1-5, 8-15, 25
1176	25	6	3	74.25	2024-10-18 08:35:17.74	Генерация: "Reagents"  1-5, 8-15, 25
1177	25	6	2	38.01	2024-12-29 13:31:05.74	Генерация: "Reagents"  1-5, 8-15, 25
1178	25	8	1	44.06	2025-11-08 11:06:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1179	25	8	2	-22.03	2025-08-11 13:35:52.74	Генерация: "Reagents"  1-5, 8-15, 25
1180	25	7	3	-12.62	2025-04-10 14:30:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1181	25	8	3	-10.78	2025-10-05 18:45:22.74	Генерация: "Reagents"  1-5, 8-15, 25
1182	25	8	2	37.17	2025-04-29 16:33:06.74	Генерация: "Reagents"  1-5, 8-15, 25
1183	25	8	2	72.80	2024-06-03 15:47:04.74	Генерация: "Reagents"  1-5, 8-15, 25
1184	25	5	3	-21.35	2026-02-25 21:42:19.74	Генерация: "Reagents"  1-5, 8-15, 25
1185	25	8	2	68.59	2025-05-10 05:01:30.74	Генерация: "Reagents"  1-5, 8-15, 25
1186	25	6	2	20.20	2025-03-28 23:08:30.74	Генерация: "Reagents"  1-5, 8-15, 25
1187	25	6	4	-30.57	2024-09-09 19:18:18.74	Генерация: "Reagents"  1-5, 8-15, 25
1188	25	8	5	94.64	2026-01-15 06:47:10.74	Генерация: "Reagents"  1-5, 8-15, 25
1189	25	5	5	30.93	2024-08-21 04:59:32.74	Генерация: "Reagents"  1-5, 8-15, 25
1190	25	5	5	-5.65	2025-01-14 10:43:00.74	Генерация: "Reagents"  1-5, 8-15, 25
1191	25	8	1	30.60	2024-12-04 12:50:27.74	Генерация: "Reagents"  1-5, 8-15, 25
1192	25	7	3	21.43	2025-07-11 12:39:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1193	25	5	2	21.82	2024-06-11 15:33:39.74	Генерация: "Reagents"  1-5, 8-15, 25
1194	25	5	1	76.70	2025-05-14 14:30:11.74	Генерация: "Reagents"  1-5, 8-15, 25
1195	25	5	3	57.77	2025-04-04 05:28:53.74	Генерация: "Reagents"  1-5, 8-15, 25
1196	25	5	2	-14.57	2024-08-20 02:13:47.74	Генерация: "Reagents"  1-5, 8-15, 25
1197	25	6	4	95.45	2025-06-15 15:47:54.74	Генерация: "Reagents"  1-5, 8-15, 25
1198	25	8	5	84.73	2025-12-06 14:37:31.74	Генерация: "Reagents"  1-5, 8-15, 25
1199	25	7	2	98.19	2026-01-04 20:08:50.74	Генерация: "Reagents"  1-5, 8-15, 25
1200	25	7	1	31.51	2025-05-21 17:38:28.74	Генерация: "Reagents"  1-5, 8-15, 25
1201	25	5	2	-2.32	2024-10-16 11:49:41.74	Генерация: "Reagents"  1-5, 8-15, 25
1202	25	7	1	42.00	2024-04-10 23:17:54.74	Генерация: "Reagents"  1-5, 8-15, 25
1203	25	6	3	18.69	2025-06-20 05:53:31.74	Генерация: "Reagents"  1-5, 8-15, 25
1204	25	7	1	1.00	2025-12-12 00:26:12.74	Генерация: "Reagents"  1-5, 8-15, 25
1205	25	8	5	67.32	2026-02-23 21:36:44.74	Генерация: "Reagents"  1-5, 8-15, 25
1206	30	6	5	79.33	2024-12-19 14:15:02.52	Генерация: "Reagents"  1-5, 8-15, 25
1207	30	7	3	-49.41	2025-07-09 15:05:43.52	Генерация: "Reagents"  1-5, 8-15, 25
1208	30	7	4	-42.08	2024-09-27 07:44:01.52	Генерация: "Reagents"  1-5, 8-15, 25
1209	30	8	4	46.89	2024-08-18 18:16:19.52	Генерация: "Reagents"  1-5, 8-15, 25
1210	30	5	2	35.07	2025-12-09 16:43:40.52	Генерация: "Reagents"  1-5, 8-15, 25
1211	30	6	5	2.65	2024-06-08 02:49:35.52	Генерация: "Reagents"  1-5, 8-15, 25
1212	30	8	5	0.73	2026-01-09 20:27:03.52	Генерация: "Reagents"  1-5, 8-15, 25
1213	30	8	2	-45.76	2025-07-31 13:08:42.52	Генерация: "Reagents"  1-5, 8-15, 25
1214	30	7	1	6.74	2024-05-31 04:32:12.52	Генерация: "Reagents"  1-5, 8-15, 25
1215	30	8	4	-0.07	2025-12-03 04:29:23.52	Генерация: "Reagents"  1-5, 8-15, 25
1216	30	8	3	33.49	2024-12-10 02:01:24.52	Генерация: "Reagents"  1-5, 8-15, 25
1217	30	7	4	-39.79	2025-04-21 14:47:50.52	Генерация: "Reagents"  1-5, 8-15, 25
1218	30	8	5	1.45	2025-05-11 10:27:32.52	Генерация: "Reagents"  1-5, 8-15, 25
1219	30	8	1	86.80	2025-10-28 06:42:37.52	Генерация: "Reagents"  1-5, 8-15, 25
1220	30	7	1	6.39	2025-03-17 19:51:06.52	Генерация: "Reagents"  1-5, 8-15, 25
1221	30	7	3	-42.77	2024-09-29 02:29:06.52	Генерация: "Reagents"  1-5, 8-15, 25
1222	30	5	4	25.09	2025-11-18 13:00:49.52	Генерация: "Reagents"  1-5, 8-15, 25
1223	30	5	4	-34.21	2024-05-30 04:22:17.52	Генерация: "Reagents"  1-5, 8-15, 25
1224	30	7	1	8.03	2025-02-23 08:02:39.52	Генерация: "Reagents"  1-5, 8-15, 25
1225	30	8	4	-1.68	2025-11-11 19:00:19.52	Генерация: "Reagents"  1-5, 8-15, 25
1226	30	8	1	79.95	2025-05-12 17:22:24.52	Генерация: "Reagents"  1-5, 8-15, 25
1227	30	8	1	73.22	2026-04-05 22:22:57.52	Генерация: "Reagents"  1-5, 8-15, 25
1228	30	6	3	39.16	2025-08-09 12:45:08.52	Генерация: "Reagents"  1-5, 8-15, 25
1229	30	8	4	81.52	2025-12-02 05:30:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1230	30	8	1	87.87	2025-04-10 08:14:31.52	Генерация: "Reagents"  1-5, 8-15, 25
1231	30	5	1	0.99	2024-06-23 20:14:27.52	Генерация: "Reagents"  1-5, 8-15, 25
1232	30	6	1	42.60	2025-06-17 17:05:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1233	30	5	3	10.32	2025-02-17 07:36:07.52	Генерация: "Reagents"  1-5, 8-15, 25
1234	30	8	4	-31.74	2024-08-02 23:35:26.52	Генерация: "Reagents"  1-5, 8-15, 25
1235	30	5	2	-10.08	2024-10-01 08:13:17.52	Генерация: "Reagents"  1-5, 8-15, 25
1236	30	5	4	40.87	2025-11-05 03:49:45.52	Генерация: "Reagents"  1-5, 8-15, 25
1237	30	8	1	68.02	2026-03-09 17:24:12.52	Генерация: "Reagents"  1-5, 8-15, 25
1238	30	8	5	49.27	2025-06-12 08:55:51.52	Генерация: "Reagents"  1-5, 8-15, 25
1239	30	8	5	-30.12	2024-08-28 12:09:26.52	Генерация: "Reagents"  1-5, 8-15, 25
1240	30	5	3	34.97	2024-04-07 17:26:56.52	Генерация: "Reagents"  1-5, 8-15, 25
1241	30	5	4	66.13	2024-04-14 01:15:42.52	Генерация: "Reagents"  1-5, 8-15, 25
1242	30	8	3	24.11	2025-08-05 01:49:02.52	Генерация: "Reagents"  1-5, 8-15, 25
1243	30	8	5	67.90	2026-03-01 15:09:56.52	Генерация: "Reagents"  1-5, 8-15, 25
1244	30	8	2	54.35	2025-08-06 22:15:29.52	Генерация: "Reagents"  1-5, 8-15, 25
1245	30	8	5	62.48	2024-11-13 20:31:03.52	Генерация: "Reagents"  1-5, 8-15, 25
1246	30	8	1	97.45	2024-11-02 03:03:32.52	Генерация: "Reagents"  1-5, 8-15, 25
1247	30	7	4	-14.49	2025-09-05 23:45:56.52	Генерация: "Reagents"  1-5, 8-15, 25
1248	30	7	2	31.55	2025-01-17 20:19:45.52	Генерация: "Reagents"  1-5, 8-15, 25
1249	30	7	5	26.53	2024-06-06 22:34:34.52	Генерация: "Reagents"  1-5, 8-15, 25
1250	30	8	2	54.37	2025-03-26 15:33:25.52	Генерация: "Reagents"  1-5, 8-15, 25
1251	30	8	4	88.97	2025-01-19 08:14:09.52	Генерация: "Reagents"  1-5, 8-15, 25
1252	30	6	4	16.24	2024-08-29 03:43:51.52	Генерация: "Reagents"  1-5, 8-15, 25
1253	30	6	4	44.66	2026-01-15 14:55:25.52	Генерация: "Reagents"  1-5, 8-15, 25
1254	30	7	2	81.67	2024-09-13 13:29:32.52	Генерация: "Reagents"  1-5, 8-15, 25
1255	30	6	1	75.25	2024-12-09 03:54:52.52	Генерация: "Reagents"  1-5, 8-15, 25
1256	30	6	5	-43.98	2025-01-16 07:05:27.52	Генерация: "Reagents"  1-5, 8-15, 25
1257	30	8	1	5.47	2024-12-09 06:29:25.52	Генерация: "Reagents"  1-5, 8-15, 25
1258	30	6	1	45.97	2024-09-28 18:08:28.52	Генерация: "Reagents"  1-5, 8-15, 25
1259	30	7	2	57.20	2024-10-16 03:41:13.52	Генерация: "Reagents"  1-5, 8-15, 25
1260	30	8	3	-36.49	2025-11-15 03:31:37.52	Генерация: "Reagents"  1-5, 8-15, 25
1261	30	5	5	58.55	2026-01-20 04:24:16.52	Генерация: "Reagents"  1-5, 8-15, 25
1262	30	6	2	-36.42	2025-04-20 01:12:33.52	Генерация: "Reagents"  1-5, 8-15, 25
1263	30	7	1	22.78	2025-05-10 20:38:40.52	Генерация: "Reagents"  1-5, 8-15, 25
1264	30	6	2	77.13	2025-01-13 15:28:30.52	Генерация: "Reagents"  1-5, 8-15, 25
1265	30	6	4	95.34	2025-02-27 22:25:36.52	Генерация: "Reagents"  1-5, 8-15, 25
1266	30	6	2	38.67	2024-06-11 14:50:00.52	Генерация: "Reagents"  1-5, 8-15, 25
1267	30	5	5	-6.67	2026-03-10 12:55:05.52	Генерация: "Reagents"  1-5, 8-15, 25
1268	30	7	4	91.22	2026-01-19 17:50:17.52	Генерация: "Reagents"  1-5, 8-15, 25
1269	30	6	4	65.20	2025-07-20 09:48:50.52	Генерация: "Reagents"  1-5, 8-15, 25
1270	30	8	4	61.27	2025-04-22 18:39:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1271	30	7	1	31.87	2025-10-12 01:17:17.52	Генерация: "Reagents"  1-5, 8-15, 25
1272	30	5	3	-48.43	2025-07-11 11:28:43.52	Генерация: "Reagents"  1-5, 8-15, 25
1273	30	5	2	99.01	2025-07-13 12:22:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1274	30	8	3	18.36	2025-09-01 04:36:28.52	Генерация: "Reagents"  1-5, 8-15, 25
1275	30	7	5	32.62	2026-04-03 00:18:47.52	Генерация: "Reagents"  1-5, 8-15, 25
1276	30	5	5	54.10	2024-12-13 14:11:42.52	Генерация: "Reagents"  1-5, 8-15, 25
1277	30	7	4	96.46	2024-05-23 20:23:37.52	Генерация: "Reagents"  1-5, 8-15, 25
1278	30	5	2	44.24	2025-04-02 14:19:51.52	Генерация: "Reagents"  1-5, 8-15, 25
1279	30	8	2	-18.63	2025-10-10 21:04:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1280	30	8	5	-20.04	2025-07-03 14:17:07.52	Генерация: "Reagents"  1-5, 8-15, 25
1281	30	8	2	-5.55	2024-08-24 08:24:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1282	30	7	3	-39.35	2026-03-16 18:43:34.52	Генерация: "Reagents"  1-5, 8-15, 25
1283	30	6	5	-20.70	2025-03-28 08:08:43.52	Генерация: "Reagents"  1-5, 8-15, 25
1284	30	6	2	-34.57	2025-12-21 16:23:05.52	Генерация: "Reagents"  1-5, 8-15, 25
1285	30	7	5	54.65	2025-03-15 03:09:44.52	Генерация: "Reagents"  1-5, 8-15, 25
1286	30	5	5	49.83	2024-09-13 23:13:21.52	Генерация: "Reagents"  1-5, 8-15, 25
1287	30	5	5	40.00	2025-08-17 04:01:28.52	Генерация: "Reagents"  1-5, 8-15, 25
1288	30	5	5	44.66	2024-10-15 10:46:42.52	Генерация: "Reagents"  1-5, 8-15, 25
1289	30	6	4	-16.73	2024-06-22 05:09:45.52	Генерация: "Reagents"  1-5, 8-15, 25
1290	30	7	5	-9.92	2025-11-12 14:18:04.52	Генерация: "Reagents"  1-5, 8-15, 25
1291	30	8	2	80.61	2025-11-28 02:40:00.52	Генерация: "Reagents"  1-5, 8-15, 25
1292	30	6	5	78.03	2024-05-04 18:04:55.52	Генерация: "Reagents"  1-5, 8-15, 25
1293	30	7	2	-38.24	2025-04-06 00:37:00.52	Генерация: "Reagents"  1-5, 8-15, 25
1294	30	6	1	34.31	2026-01-01 06:43:41.52	Генерация: "Reagents"  1-5, 8-15, 25
1295	30	8	5	18.90	2024-06-13 12:40:38.52	Генерация: "Reagents"  1-5, 8-15, 25
1296	30	5	3	49.48	2024-07-30 04:37:35.52	Генерация: "Reagents"  1-5, 8-15, 25
1297	30	5	3	-41.45	2025-06-17 13:11:29.52	Генерация: "Reagents"  1-5, 8-15, 25
1298	30	7	5	40.83	2025-12-02 15:07:12.52	Генерация: "Reagents"  1-5, 8-15, 25
1299	30	6	5	11.07	2025-08-07 21:17:01.52	Генерация: "Reagents"  1-5, 8-15, 25
1300	30	8	4	-34.10	2024-06-06 22:22:29.52	Генерация: "Reagents"  1-5, 8-15, 25
1301	30	5	5	20.46	2025-01-26 06:33:21.52	Генерация: "Reagents"  1-5, 8-15, 25
1302	30	5	5	-22.25	2025-12-06 08:09:35.52	Генерация: "Reagents"  1-5, 8-15, 25
1303	30	8	3	-26.04	2025-03-25 21:00:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1304	30	6	4	-17.64	2025-05-02 00:18:46.52	Генерация: "Reagents"  1-5, 8-15, 25
1305	30	6	5	-20.28	2026-01-29 08:16:28.52	Генерация: "Reagents"  1-5, 8-15, 25
1306	30	5	1	98.40	2024-11-26 03:47:08.52	Генерация: "Reagents"  1-5, 8-15, 25
1307	30	8	2	-28.54	2024-12-12 04:17:20.52	Генерация: "Reagents"  1-5, 8-15, 25
1308	30	6	5	14.26	2024-05-29 13:55:16.52	Генерация: "Reagents"  1-5, 8-15, 25
1309	30	6	2	68.11	2024-08-11 02:34:26.52	Генерация: "Reagents"  1-5, 8-15, 25
1310	30	7	3	-5.95	2025-02-07 13:20:31.52	Генерация: "Reagents"  1-5, 8-15, 25
1311	30	8	5	22.49	2025-07-31 04:19:00.52	Генерация: "Reagents"  1-5, 8-15, 25
1312	30	7	3	-44.03	2024-07-10 18:42:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1313	30	6	3	72.66	2026-02-16 10:43:31.52	Генерация: "Reagents"  1-5, 8-15, 25
1314	30	7	2	33.94	2024-06-11 10:11:40.52	Генерация: "Reagents"  1-5, 8-15, 25
1315	30	5	5	-32.83	2024-08-04 22:58:36.52	Генерация: "Reagents"  1-5, 8-15, 25
1316	30	8	5	-3.21	2025-05-31 08:02:21.52	Генерация: "Reagents"  1-5, 8-15, 25
1317	30	8	2	81.38	2026-02-13 14:40:20.52	Генерация: "Reagents"  1-5, 8-15, 25
1318	30	6	5	-15.07	2024-09-05 16:02:40.52	Генерация: "Reagents"  1-5, 8-15, 25
1319	30	8	3	53.74	2025-02-18 08:58:19.52	Генерация: "Reagents"  1-5, 8-15, 25
1320	30	5	2	49.72	2024-12-20 16:40:27.52	Генерация: "Reagents"  1-5, 8-15, 25
1321	30	5	4	7.53	2025-12-11 05:27:30.52	Генерация: "Reagents"  1-5, 8-15, 25
1322	30	7	4	33.58	2024-12-08 18:50:07.52	Генерация: "Reagents"  1-5, 8-15, 25
1323	30	6	3	4.68	2024-10-15 01:37:12.52	Генерация: "Reagents"  1-5, 8-15, 25
1324	30	5	3	-20.59	2024-09-02 21:05:21.52	Генерация: "Reagents"  1-5, 8-15, 25
1325	30	8	1	9.68	2024-07-27 07:47:58.52	Генерация: "Reagents"  1-5, 8-15, 25
1326	30	8	5	30.79	2025-10-16 11:24:08.52	Генерация: "Reagents"  1-5, 8-15, 25
1327	30	6	1	26.08	2025-12-13 00:09:37.52	Генерация: "Reagents"  1-5, 8-15, 25
1328	30	6	1	63.61	2024-05-02 01:03:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1329	30	6	5	42.90	2025-04-12 19:30:31.52	Генерация: "Reagents"  1-5, 8-15, 25
1330	30	5	3	-33.46	2025-03-23 10:17:24.52	Генерация: "Reagents"  1-5, 8-15, 25
1331	30	5	1	89.88	2025-08-02 00:44:43.52	Генерация: "Reagents"  1-5, 8-15, 25
1332	30	6	1	74.92	2025-10-09 17:17:51.52	Генерация: "Reagents"  1-5, 8-15, 25
1333	30	6	4	-28.33	2025-02-17 16:16:22.52	Генерация: "Reagents"  1-5, 8-15, 25
1334	30	8	2	-3.91	2025-04-26 22:27:04.52	Генерация: "Reagents"  1-5, 8-15, 25
1335	30	6	3	42.70	2026-01-08 00:30:02.52	Генерация: "Reagents"  1-5, 8-15, 25
1336	30	7	2	-37.27	2025-02-14 16:37:02.52	Генерация: "Reagents"  1-5, 8-15, 25
1337	30	5	2	-49.80	2025-05-30 09:48:45.52	Генерация: "Reagents"  1-5, 8-15, 25
1338	30	6	3	56.37	2025-02-09 21:41:44.52	Генерация: "Reagents"  1-5, 8-15, 25
1339	30	6	4	-48.04	2024-05-11 18:22:38.52	Генерация: "Reagents"  1-5, 8-15, 25
1340	30	7	3	-47.41	2025-04-28 11:09:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1341	30	7	5	58.70	2026-04-07 04:12:47.52	Генерация: "Reagents"  1-5, 8-15, 25
1342	30	6	1	74.72	2026-04-06 16:24:16.52	Генерация: "Reagents"  1-5, 8-15, 25
1343	30	5	2	61.05	2025-09-02 05:59:19.52	Генерация: "Reagents"  1-5, 8-15, 25
1344	30	8	3	33.99	2024-07-02 11:31:39.52	Генерация: "Reagents"  1-5, 8-15, 25
1345	30	6	3	63.04	2024-12-27 11:16:20.52	Генерация: "Reagents"  1-5, 8-15, 25
1346	30	7	1	11.95	2024-10-08 19:45:35.52	Генерация: "Reagents"  1-5, 8-15, 25
1347	30	8	4	39.68	2025-08-25 06:37:21.52	Генерация: "Reagents"  1-5, 8-15, 25
1348	30	8	1	33.04	2025-09-24 14:06:36.52	Генерация: "Reagents"  1-5, 8-15, 25
1349	30	5	2	-46.75	2025-06-18 22:20:48.52	Генерация: "Reagents"  1-5, 8-15, 25
1350	30	5	2	80.01	2024-05-18 04:52:46.52	Генерация: "Reagents"  1-5, 8-15, 25
1351	30	8	1	37.99	2024-05-07 01:06:33.52	Генерация: "Reagents"  1-5, 8-15, 25
1352	30	5	5	49.94	2024-12-23 01:59:28.52	Генерация: "Reagents"  1-5, 8-15, 25
1353	30	7	5	31.30	2025-09-25 00:29:57.52	Генерация: "Reagents"  1-5, 8-15, 25
1354	30	7	4	83.97	2025-01-12 16:03:36.52	Генерация: "Reagents"  1-5, 8-15, 25
1355	30	7	1	57.66	2025-05-31 00:50:00.52	Генерация: "Reagents"  1-5, 8-15, 25
1356	31	7	2	12.32	2025-09-29 17:03:52.85	Генерация: "Reagents"  1-5, 8-15, 25
1357	31	6	4	29.83	2025-01-09 07:18:52.85	Генерация: "Reagents"  1-5, 8-15, 25
1358	31	8	2	-49.38	2025-11-12 15:20:31.85	Генерация: "Reagents"  1-5, 8-15, 25
1359	31	7	2	-2.62	2025-06-06 16:58:49.85	Генерация: "Reagents"  1-5, 8-15, 25
1360	31	8	1	29.69	2024-07-04 07:29:12.85	Генерация: "Reagents"  1-5, 8-15, 25
1361	31	6	5	63.29	2025-10-21 20:55:27.85	Генерация: "Reagents"  1-5, 8-15, 25
1362	31	8	1	20.50	2024-04-18 12:08:05.85	Генерация: "Reagents"  1-5, 8-15, 25
1363	31	7	2	29.79	2025-11-26 14:11:06.85	Генерация: "Reagents"  1-5, 8-15, 25
1364	31	7	1	66.96	2025-07-28 00:47:43.85	Генерация: "Reagents"  1-5, 8-15, 25
1365	31	6	2	-41.22	2025-04-03 12:00:19.85	Генерация: "Reagents"  1-5, 8-15, 25
1366	31	8	4	-8.55	2025-09-29 04:39:45.85	Генерация: "Reagents"  1-5, 8-15, 25
1367	31	6	2	64.95	2025-02-04 06:10:46.85	Генерация: "Reagents"  1-5, 8-15, 25
1368	31	7	5	84.18	2026-02-07 16:25:13.85	Генерация: "Reagents"  1-5, 8-15, 25
1369	31	7	1	54.13	2024-05-13 18:38:47.85	Генерация: "Reagents"  1-5, 8-15, 25
1370	31	7	4	72.89	2025-03-06 05:12:34.85	Генерация: "Reagents"  1-5, 8-15, 25
1371	31	7	5	96.16	2025-01-05 16:27:34.85	Генерация: "Reagents"  1-5, 8-15, 25
1372	31	8	4	1.87	2024-09-15 19:56:28.85	Генерация: "Reagents"  1-5, 8-15, 25
1373	31	8	1	64.58	2026-03-25 16:17:29.85	Генерация: "Reagents"  1-5, 8-15, 25
1374	31	6	1	22.27	2024-05-18 07:59:28.85	Генерация: "Reagents"  1-5, 8-15, 25
1375	31	7	2	-29.49	2026-01-03 06:28:40.85	Генерация: "Reagents"  1-5, 8-15, 25
1376	31	6	4	42.20	2024-11-29 21:44:41.85	Генерация: "Reagents"  1-5, 8-15, 25
1377	31	7	3	8.96	2025-11-27 23:32:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1378	31	8	4	47.43	2024-10-17 15:01:42.85	Генерация: "Reagents"  1-5, 8-15, 25
1379	31	5	4	-16.90	2026-03-23 04:14:19.85	Генерация: "Reagents"  1-5, 8-15, 25
1380	31	5	1	80.75	2024-09-23 04:34:07.85	Генерация: "Reagents"  1-5, 8-15, 25
1381	31	8	1	24.62	2026-02-08 15:35:42.85	Генерация: "Reagents"  1-5, 8-15, 25
1382	31	6	1	75.01	2024-07-16 03:08:11.85	Генерация: "Reagents"  1-5, 8-15, 25
1383	31	6	2	68.70	2025-10-07 08:28:44.85	Генерация: "Reagents"  1-5, 8-15, 25
1384	31	5	5	22.51	2025-05-12 17:17:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1385	31	7	4	22.98	2024-04-09 06:01:39.85	Генерация: "Reagents"  1-5, 8-15, 25
1386	31	7	4	-19.42	2025-03-07 16:35:39.85	Генерация: "Reagents"  1-5, 8-15, 25
1387	31	7	2	-42.22	2025-05-11 21:50:13.85	Генерация: "Reagents"  1-5, 8-15, 25
1388	31	6	3	59.46	2025-11-17 00:33:47.85	Генерация: "Reagents"  1-5, 8-15, 25
1389	31	5	5	10.97	2025-03-09 06:24:18.85	Генерация: "Reagents"  1-5, 8-15, 25
1390	31	8	5	64.20	2024-06-01 03:16:19.85	Генерация: "Reagents"  1-5, 8-15, 25
1391	31	5	4	48.90	2025-11-14 02:56:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1392	31	8	2	-47.38	2025-07-12 23:12:41.85	Генерация: "Reagents"  1-5, 8-15, 25
1393	31	6	3	-4.43	2025-12-14 01:33:52.85	Генерация: "Reagents"  1-5, 8-15, 25
1394	31	5	2	-47.36	2025-04-02 07:40:11.85	Генерация: "Reagents"  1-5, 8-15, 25
1395	31	8	1	11.59	2024-08-31 20:21:33.85	Генерация: "Reagents"  1-5, 8-15, 25
1396	31	7	2	-38.84	2025-06-19 06:40:09.85	Генерация: "Reagents"  1-5, 8-15, 25
1397	31	5	2	-15.65	2024-10-12 14:05:13.85	Генерация: "Reagents"  1-5, 8-15, 25
1398	31	6	2	9.08	2025-11-23 04:12:01.85	Генерация: "Reagents"  1-5, 8-15, 25
1399	31	8	1	0.48	2024-07-12 02:26:40.85	Генерация: "Reagents"  1-5, 8-15, 25
1400	31	5	5	52.22	2026-01-08 01:28:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1401	31	7	3	59.07	2026-03-30 19:49:23.85	Генерация: "Reagents"  1-5, 8-15, 25
1402	31	8	2	45.56	2025-02-02 05:46:13.85	Генерация: "Reagents"  1-5, 8-15, 25
1403	31	5	3	20.03	2026-03-28 10:53:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1404	31	8	5	-8.87	2024-12-13 20:35:43.85	Генерация: "Reagents"  1-5, 8-15, 25
1405	31	5	2	-20.74	2026-03-30 03:42:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1406	31	7	2	3.30	2024-09-30 04:51:59.85	Генерация: "Reagents"  1-5, 8-15, 25
1407	31	7	3	30.69	2026-02-15 10:07:34.85	Генерация: "Reagents"  1-5, 8-15, 25
1408	31	5	3	55.54	2025-09-26 11:35:54.85	Генерация: "Reagents"  1-5, 8-15, 25
1409	31	8	5	-46.16	2025-10-03 16:47:53.85	Генерация: "Reagents"  1-5, 8-15, 25
1410	31	5	5	25.09	2025-10-12 05:13:53.85	Генерация: "Reagents"  1-5, 8-15, 25
1411	31	8	1	58.53	2024-06-28 07:32:39.85	Генерация: "Reagents"  1-5, 8-15, 25
1412	31	7	3	80.85	2024-09-18 05:51:35.85	Генерация: "Reagents"  1-5, 8-15, 25
1413	31	8	4	83.90	2025-05-28 14:51:54.85	Генерация: "Reagents"  1-5, 8-15, 25
1414	31	7	4	61.24	2025-08-22 12:55:57.85	Генерация: "Reagents"  1-5, 8-15, 25
1415	31	7	4	66.96	2024-12-13 12:49:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1416	31	6	3	-31.84	2026-01-07 08:43:16.85	Генерация: "Reagents"  1-5, 8-15, 25
1417	31	5	5	20.13	2024-06-16 03:04:17.85	Генерация: "Reagents"  1-5, 8-15, 25
1418	31	5	3	-45.04	2024-08-11 23:28:18.85	Генерация: "Reagents"  1-5, 8-15, 25
1419	31	6	5	72.79	2025-08-16 02:38:25.85	Генерация: "Reagents"  1-5, 8-15, 25
1420	31	5	2	-8.17	2025-02-09 06:22:05.85	Генерация: "Reagents"  1-5, 8-15, 25
1421	31	6	2	-29.01	2025-11-29 12:50:37.85	Генерация: "Reagents"  1-5, 8-15, 25
1422	31	7	5	42.55	2024-09-17 14:12:57.85	Генерация: "Reagents"  1-5, 8-15, 25
1423	31	7	5	77.19	2025-09-14 09:56:03.85	Генерация: "Reagents"  1-5, 8-15, 25
1424	31	5	2	-32.51	2026-04-01 01:38:02.85	Генерация: "Reagents"  1-5, 8-15, 25
1425	31	6	3	60.90	2024-12-29 20:18:47.85	Генерация: "Reagents"  1-5, 8-15, 25
1426	31	7	2	37.44	2024-09-20 16:24:34.85	Генерация: "Reagents"  1-5, 8-15, 25
1427	31	7	4	-7.35	2025-08-21 09:35:45.85	Генерация: "Reagents"  1-5, 8-15, 25
1428	31	7	5	70.46	2026-02-27 22:50:20.85	Генерация: "Reagents"  1-5, 8-15, 25
1429	31	8	2	93.97	2025-03-11 18:13:29.85	Генерация: "Reagents"  1-5, 8-15, 25
1430	31	7	3	84.27	2025-01-05 03:21:05.85	Генерация: "Reagents"  1-5, 8-15, 25
1431	31	5	4	49.62	2025-07-12 10:47:57.85	Генерация: "Reagents"  1-5, 8-15, 25
1432	31	8	1	78.77	2025-01-06 14:53:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1433	31	8	1	47.39	2025-11-29 21:36:20.85	Генерация: "Reagents"  1-5, 8-15, 25
1434	31	5	1	46.20	2025-04-22 00:16:40.85	Генерация: "Reagents"  1-5, 8-15, 25
1435	31	8	5	53.58	2025-12-30 03:26:27.85	Генерация: "Reagents"  1-5, 8-15, 25
1436	31	7	1	49.42	2024-11-06 05:48:02.85	Генерация: "Reagents"  1-5, 8-15, 25
1437	31	8	3	5.14	2025-05-25 03:47:22.85	Генерация: "Reagents"  1-5, 8-15, 25
1438	31	8	3	-43.65	2025-04-05 19:16:24.85	Генерация: "Reagents"  1-5, 8-15, 25
1439	31	5	3	-27.95	2025-02-10 01:39:08.85	Генерация: "Reagents"  1-5, 8-15, 25
1440	31	7	1	27.86	2025-07-30 19:48:13.85	Генерация: "Reagents"  1-5, 8-15, 25
1441	31	5	5	1.75	2025-12-06 22:47:57.85	Генерация: "Reagents"  1-5, 8-15, 25
1442	31	5	2	25.67	2025-03-23 21:22:42.85	Генерация: "Reagents"  1-5, 8-15, 25
1443	31	5	5	33.26	2024-07-19 11:37:40.85	Генерация: "Reagents"  1-5, 8-15, 25
1444	31	6	1	38.53	2024-09-26 14:04:53.85	Генерация: "Reagents"  1-5, 8-15, 25
1445	31	8	2	68.23	2025-09-30 04:07:35.85	Генерация: "Reagents"  1-5, 8-15, 25
1446	31	5	2	27.58	2024-04-09 15:57:00.85	Генерация: "Reagents"  1-5, 8-15, 25
1447	31	8	1	97.99	2025-05-31 16:12:22.85	Генерация: "Reagents"  1-5, 8-15, 25
1448	31	7	1	26.80	2024-06-17 10:18:48.85	Генерация: "Reagents"  1-5, 8-15, 25
1449	31	5	1	91.07	2026-01-07 21:31:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1450	31	8	4	46.67	2025-12-12 13:35:00.85	Генерация: "Reagents"  1-5, 8-15, 25
1451	31	7	1	85.03	2024-06-02 11:02:49.85	Генерация: "Reagents"  1-5, 8-15, 25
1452	31	6	4	-21.71	2024-06-17 16:38:20.85	Генерация: "Reagents"  1-5, 8-15, 25
1453	31	7	3	31.67	2025-12-09 11:11:42.85	Генерация: "Reagents"  1-5, 8-15, 25
1454	31	8	1	97.43	2025-09-06 04:50:46.85	Генерация: "Reagents"  1-5, 8-15, 25
1455	31	6	5	16.87	2024-11-10 07:21:20.85	Генерация: "Reagents"  1-5, 8-15, 25
1456	31	5	4	-4.37	2024-09-17 21:44:05.85	Генерация: "Reagents"  1-5, 8-15, 25
1457	31	7	2	39.69	2025-03-20 11:35:21.85	Генерация: "Reagents"  1-5, 8-15, 25
1458	31	6	3	63.76	2024-11-02 09:55:46.85	Генерация: "Reagents"  1-5, 8-15, 25
1459	31	8	3	-20.20	2024-11-28 19:46:19.85	Генерация: "Reagents"  1-5, 8-15, 25
1460	31	7	2	81.85	2025-02-07 22:20:48.85	Генерация: "Reagents"  1-5, 8-15, 25
1461	31	7	3	19.03	2024-08-17 23:25:17.85	Генерация: "Reagents"  1-5, 8-15, 25
1462	31	8	4	87.12	2025-05-20 20:23:38.85	Генерация: "Reagents"  1-5, 8-15, 25
1463	31	8	3	63.15	2024-11-26 01:49:45.85	Генерация: "Reagents"  1-5, 8-15, 25
1464	31	8	3	-0.75	2026-01-11 18:55:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1465	31	6	4	40.54	2025-05-01 07:45:47.85	Генерация: "Reagents"  1-5, 8-15, 25
1466	31	5	2	67.93	2025-05-18 19:06:55.85	Генерация: "Reagents"  1-5, 8-15, 25
1467	31	5	2	4.75	2024-10-28 09:19:51.85	Генерация: "Reagents"  1-5, 8-15, 25
1468	31	5	4	43.28	2025-10-31 00:06:35.85	Генерация: "Reagents"  1-5, 8-15, 25
1469	31	5	3	29.08	2025-03-14 04:14:01.85	Генерация: "Reagents"  1-5, 8-15, 25
1470	31	5	3	64.42	2024-10-22 06:29:10.85	Генерация: "Reagents"  1-5, 8-15, 25
1471	31	6	1	41.77	2024-08-22 20:11:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1472	31	7	4	-8.88	2025-10-10 02:52:48.85	Генерация: "Reagents"  1-5, 8-15, 25
1473	31	7	2	5.64	2024-06-24 05:19:33.85	Генерация: "Reagents"  1-5, 8-15, 25
1474	31	8	1	44.72	2026-02-15 21:12:47.85	Генерация: "Reagents"  1-5, 8-15, 25
1475	31	5	3	-48.58	2024-11-04 00:55:09.85	Генерация: "Reagents"  1-5, 8-15, 25
1476	31	8	3	4.73	2024-08-01 15:24:33.85	Генерация: "Reagents"  1-5, 8-15, 25
1477	31	6	5	45.01	2024-12-07 03:00:36.85	Генерация: "Reagents"  1-5, 8-15, 25
1478	31	6	3	15.26	2025-09-21 12:07:23.85	Генерация: "Reagents"  1-5, 8-15, 25
1479	31	7	2	-45.78	2024-09-18 23:10:29.85	Генерация: "Reagents"  1-5, 8-15, 25
1480	31	8	2	7.31	2024-10-12 01:22:58.85	Генерация: "Reagents"  1-5, 8-15, 25
1481	31	6	5	-48.89	2025-02-18 08:53:24.85	Генерация: "Reagents"  1-5, 8-15, 25
1482	31	8	2	-44.10	2025-03-24 02:59:17.85	Генерация: "Reagents"  1-5, 8-15, 25
1483	31	8	1	26.85	2024-08-22 20:09:16.85	Генерация: "Reagents"  1-5, 8-15, 25
1484	31	5	3	75.88	2025-10-23 01:16:51.85	Генерация: "Reagents"  1-5, 8-15, 25
1485	31	6	3	-37.72	2025-10-06 11:47:05.85	Генерация: "Reagents"  1-5, 8-15, 25
1486	31	7	4	24.77	2026-01-12 19:48:15.85	Генерация: "Reagents"  1-5, 8-15, 25
1487	31	8	4	74.67	2026-01-29 06:36:45.85	Генерация: "Reagents"  1-5, 8-15, 25
1488	31	5	2	96.99	2024-12-18 18:12:15.85	Генерация: "Reagents"  1-5, 8-15, 25
1489	31	6	1	6.42	2025-04-21 09:23:59.85	Генерация: "Reagents"  1-5, 8-15, 25
1490	31	7	1	26.92	2025-07-23 22:38:45.85	Генерация: "Reagents"  1-5, 8-15, 25
1491	31	7	5	37.18	2025-10-11 20:10:42.85	Генерация: "Reagents"  1-5, 8-15, 25
1492	31	6	3	61.10	2025-07-19 11:08:06.85	Генерация: "Reagents"  1-5, 8-15, 25
1493	31	8	4	53.28	2024-07-10 20:17:59.85	Генерация: "Reagents"  1-5, 8-15, 25
1494	31	7	1	33.84	2025-12-03 10:27:31.85	Генерация: "Reagents"  1-5, 8-15, 25
1495	31	7	5	-30.74	2025-02-07 11:30:48.85	Генерация: "Reagents"  1-5, 8-15, 25
1496	31	6	1	15.50	2025-11-01 17:29:17.85	Генерация: "Reagents"  1-5, 8-15, 25
1497	31	7	5	52.53	2024-12-18 17:45:35.85	Генерация: "Reagents"  1-5, 8-15, 25
1498	31	7	4	62.10	2025-08-15 05:24:40.85	Генерация: "Reagents"  1-5, 8-15, 25
1499	31	8	5	47.88	2026-02-09 17:28:21.85	Генерация: "Reagents"  1-5, 8-15, 25
1500	31	5	5	-23.18	2025-09-30 21:39:15.85	Генерация: "Reagents"  1-5, 8-15, 25
1501	31	7	4	-16.39	2025-10-01 23:33:28.85	Генерация: "Reagents"  1-5, 8-15, 25
1502	31	5	5	8.66	2025-04-12 19:44:38.85	Генерация: "Reagents"  1-5, 8-15, 25
1503	31	6	5	24.26	2025-03-18 22:51:20.85	Генерация: "Reagents"  1-5, 8-15, 25
1504	31	5	4	96.63	2025-04-01 05:16:07.85	Генерация: "Reagents"  1-5, 8-15, 25
1505	31	7	2	-7.12	2025-11-27 18:34:32.85	Генерация: "Reagents"  1-5, 8-15, 25
1506	32	8	4	47.91	2024-05-31 08:21:05.28	Генерация: "Reagents"  1-5, 8-15, 25
1507	32	8	3	70.41	2025-01-21 19:36:10.28	Генерация: "Reagents"  1-5, 8-15, 25
1508	32	8	4	88.13	2024-08-14 02:32:14.28	Генерация: "Reagents"  1-5, 8-15, 25
1509	32	7	1	99.85	2024-04-13 15:19:50.28	Генерация: "Reagents"  1-5, 8-15, 25
1510	32	6	1	98.47	2024-11-23 08:48:51.28	Генерация: "Reagents"  1-5, 8-15, 25
1511	32	8	5	-44.86	2025-02-14 19:29:29.28	Генерация: "Reagents"  1-5, 8-15, 25
1512	32	8	4	-8.29	2025-06-02 19:03:31.28	Генерация: "Reagents"  1-5, 8-15, 25
1513	32	5	3	-19.93	2024-12-26 16:17:02.28	Генерация: "Reagents"  1-5, 8-15, 25
1514	32	5	4	32.36	2024-06-21 04:20:02.28	Генерация: "Reagents"  1-5, 8-15, 25
1515	32	6	5	-18.33	2024-05-04 21:50:02.28	Генерация: "Reagents"  1-5, 8-15, 25
1516	32	7	1	23.20	2025-10-20 21:19:25.28	Генерация: "Reagents"  1-5, 8-15, 25
1517	32	8	3	-46.39	2025-11-09 17:30:41.28	Генерация: "Reagents"  1-5, 8-15, 25
1518	32	5	4	-10.08	2024-12-09 11:27:05.28	Генерация: "Reagents"  1-5, 8-15, 25
1519	32	6	2	6.11	2024-08-07 10:11:32.28	Генерация: "Reagents"  1-5, 8-15, 25
1520	32	6	4	42.47	2024-04-18 22:54:25.28	Генерация: "Reagents"  1-5, 8-15, 25
1521	32	7	4	-41.13	2024-10-17 06:48:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1522	32	7	4	85.10	2024-12-04 22:37:14.28	Генерация: "Reagents"  1-5, 8-15, 25
1523	32	8	5	41.66	2025-09-14 03:51:17.28	Генерация: "Reagents"  1-5, 8-15, 25
1524	32	7	3	-26.74	2024-11-08 04:15:33.28	Генерация: "Reagents"  1-5, 8-15, 25
1525	32	6	3	78.49	2024-12-28 22:57:06.28	Генерация: "Reagents"  1-5, 8-15, 25
1526	32	8	1	89.50	2025-03-24 23:25:44.28	Генерация: "Reagents"  1-5, 8-15, 25
1527	32	5	2	25.34	2025-09-15 13:08:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1528	32	6	1	26.16	2025-11-05 18:26:27.28	Генерация: "Reagents"  1-5, 8-15, 25
1529	32	5	5	31.93	2025-08-10 16:12:13.28	Генерация: "Reagents"  1-5, 8-15, 25
1530	32	6	3	-7.65	2024-04-09 05:57:33.28	Генерация: "Reagents"  1-5, 8-15, 25
1531	32	8	4	62.97	2026-03-26 05:00:20.28	Генерация: "Reagents"  1-5, 8-15, 25
1532	32	7	2	37.61	2024-04-21 01:13:03.28	Генерация: "Reagents"  1-5, 8-15, 25
1533	32	5	3	-19.62	2025-09-02 01:36:53.28	Генерация: "Reagents"  1-5, 8-15, 25
1534	32	5	2	90.77	2025-08-24 01:21:37.28	Генерация: "Reagents"  1-5, 8-15, 25
1535	32	8	5	38.72	2025-07-21 02:12:35.28	Генерация: "Reagents"  1-5, 8-15, 25
1536	32	7	5	56.35	2024-05-29 13:12:21.28	Генерация: "Reagents"  1-5, 8-15, 25
1537	32	5	1	0.84	2025-10-23 09:38:41.28	Генерация: "Reagents"  1-5, 8-15, 25
1538	32	8	2	-16.13	2025-12-10 05:29:08.28	Генерация: "Reagents"  1-5, 8-15, 25
1539	32	8	3	23.53	2024-09-15 03:55:41.28	Генерация: "Reagents"  1-5, 8-15, 25
1540	32	5	4	35.89	2025-08-10 01:07:11.28	Генерация: "Reagents"  1-5, 8-15, 25
1541	32	8	5	-28.01	2025-01-18 08:33:23.28	Генерация: "Reagents"  1-5, 8-15, 25
1542	32	6	2	68.81	2026-02-03 21:33:16.28	Генерация: "Reagents"  1-5, 8-15, 25
1543	32	8	4	88.61	2025-08-08 22:05:51.28	Генерация: "Reagents"  1-5, 8-15, 25
1544	32	5	5	14.12	2026-03-18 19:00:40.28	Генерация: "Reagents"  1-5, 8-15, 25
1545	32	8	1	87.44	2026-04-01 16:55:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1546	32	5	4	66.26	2024-06-17 16:25:31.28	Генерация: "Reagents"  1-5, 8-15, 25
1547	32	5	5	99.50	2026-04-07 04:58:35.28	Генерация: "Reagents"  1-5, 8-15, 25
1548	32	5	5	51.92	2024-07-10 17:35:46.28	Генерация: "Reagents"  1-5, 8-15, 25
1549	32	7	2	78.17	2024-09-23 10:10:19.28	Генерация: "Reagents"  1-5, 8-15, 25
1550	32	5	2	35.15	2025-08-15 12:36:01.28	Генерация: "Reagents"  1-5, 8-15, 25
1551	32	5	4	93.68	2026-03-18 16:10:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1552	32	5	5	41.05	2025-09-20 17:49:16.28	Генерация: "Reagents"  1-5, 8-15, 25
1553	32	6	1	11.54	2024-07-12 00:28:28.28	Генерация: "Reagents"  1-5, 8-15, 25
1554	32	8	3	-2.33	2025-10-27 07:02:40.28	Генерация: "Reagents"  1-5, 8-15, 25
1555	32	6	4	-7.02	2025-07-19 00:15:46.28	Генерация: "Reagents"  1-5, 8-15, 25
1556	32	6	2	53.65	2024-09-26 04:12:34.28	Генерация: "Reagents"  1-5, 8-15, 25
1557	32	6	5	-16.85	2024-11-20 15:22:03.28	Генерация: "Reagents"  1-5, 8-15, 25
1558	32	8	2	-7.58	2026-02-12 11:26:35.28	Генерация: "Reagents"  1-5, 8-15, 25
1559	32	6	2	46.95	2024-10-26 08:19:20.28	Генерация: "Reagents"  1-5, 8-15, 25
1560	32	7	2	8.45	2026-04-06 00:02:40.28	Генерация: "Reagents"  1-5, 8-15, 25
1561	32	5	3	15.74	2026-01-08 04:53:17.28	Генерация: "Reagents"  1-5, 8-15, 25
1562	32	5	2	-21.53	2026-01-13 08:21:32.28	Генерация: "Reagents"  1-5, 8-15, 25
1563	32	7	1	8.16	2025-05-24 02:53:26.28	Генерация: "Reagents"  1-5, 8-15, 25
1564	32	5	1	37.82	2024-08-27 23:07:19.28	Генерация: "Reagents"  1-5, 8-15, 25
1565	32	7	4	-7.37	2025-08-09 21:03:34.28	Генерация: "Reagents"  1-5, 8-15, 25
1566	32	7	2	-38.21	2025-10-04 00:58:20.28	Генерация: "Reagents"  1-5, 8-15, 25
1567	32	7	3	91.65	2025-02-01 20:30:08.28	Генерация: "Reagents"  1-5, 8-15, 25
1568	32	6	1	59.02	2026-02-11 16:13:50.28	Генерация: "Reagents"  1-5, 8-15, 25
1569	32	8	5	9.04	2025-02-11 09:55:16.28	Генерация: "Reagents"  1-5, 8-15, 25
1570	32	5	3	77.27	2025-08-02 03:51:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1571	32	6	5	-17.33	2024-08-15 04:08:17.28	Генерация: "Reagents"  1-5, 8-15, 25
1572	32	7	3	-2.13	2025-07-28 01:40:36.28	Генерация: "Reagents"  1-5, 8-15, 25
1573	32	5	1	33.60	2024-06-29 02:34:03.28	Генерация: "Reagents"  1-5, 8-15, 25
1574	32	8	5	58.51	2025-09-01 04:48:51.28	Генерация: "Reagents"  1-5, 8-15, 25
1575	32	5	2	-36.74	2026-03-02 02:45:36.28	Генерация: "Reagents"  1-5, 8-15, 25
1576	32	6	4	35.70	2024-04-22 11:30:37.28	Генерация: "Reagents"  1-5, 8-15, 25
1577	32	6	3	26.08	2025-08-29 20:22:44.28	Генерация: "Reagents"  1-5, 8-15, 25
1578	32	5	1	91.20	2025-01-09 18:22:06.28	Генерация: "Reagents"  1-5, 8-15, 25
1579	32	6	2	50.14	2026-01-22 06:23:54.28	Генерация: "Reagents"  1-5, 8-15, 25
1580	32	5	5	75.98	2024-10-24 08:22:51.28	Генерация: "Reagents"  1-5, 8-15, 25
1581	32	5	3	93.02	2024-08-28 18:37:17.28	Генерация: "Reagents"  1-5, 8-15, 25
1582	32	6	4	-16.44	2025-02-20 23:27:37.28	Генерация: "Reagents"  1-5, 8-15, 25
1583	32	7	1	16.44	2026-03-27 06:36:02.28	Генерация: "Reagents"  1-5, 8-15, 25
1584	32	5	3	62.94	2025-05-03 02:07:03.28	Генерация: "Reagents"  1-5, 8-15, 25
1585	32	7	4	54.29	2025-06-04 13:08:13.28	Генерация: "Reagents"  1-5, 8-15, 25
1586	32	8	4	-41.21	2025-05-14 04:10:10.28	Генерация: "Reagents"  1-5, 8-15, 25
1587	32	6	1	57.71	2025-05-07 03:13:11.28	Генерация: "Reagents"  1-5, 8-15, 25
1588	32	6	4	-13.53	2025-09-02 07:31:09.28	Генерация: "Reagents"  1-5, 8-15, 25
1589	32	7	2	-2.12	2024-04-27 16:54:30.28	Генерация: "Reagents"  1-5, 8-15, 25
1590	32	6	3	83.04	2026-01-08 11:02:49.28	Генерация: "Reagents"  1-5, 8-15, 25
1591	32	7	1	34.46	2025-03-11 09:51:21.28	Генерация: "Reagents"  1-5, 8-15, 25
1592	32	8	3	50.37	2024-08-06 06:47:54.28	Генерация: "Reagents"  1-5, 8-15, 25
1593	32	7	5	25.49	2024-07-23 20:35:14.28	Генерация: "Reagents"  1-5, 8-15, 25
1594	32	5	1	48.32	2024-09-03 08:22:25.28	Генерация: "Reagents"  1-5, 8-15, 25
1595	32	5	1	25.69	2026-03-10 17:21:52.28	Генерация: "Reagents"  1-5, 8-15, 25
1596	32	5	2	19.49	2024-07-15 01:10:20.28	Генерация: "Reagents"  1-5, 8-15, 25
1597	32	5	1	28.36	2025-09-12 03:28:32.28	Генерация: "Reagents"  1-5, 8-15, 25
1598	32	5	4	-18.99	2025-06-05 18:10:23.28	Генерация: "Reagents"  1-5, 8-15, 25
1599	32	7	5	-31.08	2025-11-29 01:36:10.28	Генерация: "Reagents"  1-5, 8-15, 25
1600	32	6	5	68.93	2024-06-16 06:58:53.28	Генерация: "Reagents"  1-5, 8-15, 25
1601	32	6	1	78.00	2024-09-05 14:58:58.28	Генерация: "Reagents"  1-5, 8-15, 25
1602	32	6	5	90.88	2026-03-22 00:09:12.28	Генерация: "Reagents"  1-5, 8-15, 25
1603	32	6	5	37.99	2025-07-29 09:03:23.28	Генерация: "Reagents"  1-5, 8-15, 25
1604	32	8	4	37.06	2024-10-10 04:35:25.28	Генерация: "Reagents"  1-5, 8-15, 25
1605	32	6	1	24.59	2025-11-16 04:17:51.28	Генерация: "Reagents"  1-5, 8-15, 25
1606	32	8	4	91.99	2025-10-19 05:42:47.28	Генерация: "Reagents"  1-5, 8-15, 25
1607	32	6	3	48.70	2026-03-12 06:34:52.28	Генерация: "Reagents"  1-5, 8-15, 25
1608	32	5	3	-43.89	2024-07-11 06:35:00.28	Генерация: "Reagents"  1-5, 8-15, 25
1609	32	5	2	-7.81	2024-05-25 17:01:08.28	Генерация: "Reagents"  1-5, 8-15, 25
1610	32	6	2	7.53	2025-06-10 09:46:04.28	Генерация: "Reagents"  1-5, 8-15, 25
1611	32	5	1	27.83	2024-06-14 17:34:11.28	Генерация: "Reagents"  1-5, 8-15, 25
1612	32	6	5	37.35	2026-01-21 14:09:28.28	Генерация: "Reagents"  1-5, 8-15, 25
1613	32	6	1	22.69	2024-09-23 20:23:58.28	Генерация: "Reagents"  1-5, 8-15, 25
1614	32	8	1	11.59	2024-09-01 13:31:33.28	Генерация: "Reagents"  1-5, 8-15, 25
1615	32	7	1	11.73	2025-04-07 07:01:57.28	Генерация: "Reagents"  1-5, 8-15, 25
1616	32	7	1	11.43	2026-02-27 10:33:14.28	Генерация: "Reagents"  1-5, 8-15, 25
1617	32	6	2	83.52	2025-03-05 23:00:01.28	Генерация: "Reagents"  1-5, 8-15, 25
1618	32	7	5	34.72	2025-09-15 04:50:56.28	Генерация: "Reagents"  1-5, 8-15, 25
1619	32	5	5	63.68	2025-01-24 06:18:57.28	Генерация: "Reagents"  1-5, 8-15, 25
1620	32	6	4	21.30	2024-12-23 06:40:32.28	Генерация: "Reagents"  1-5, 8-15, 25
1621	32	8	5	53.79	2025-04-27 07:58:04.28	Генерация: "Reagents"  1-5, 8-15, 25
1622	32	8	4	87.92	2025-05-14 12:09:50.28	Генерация: "Reagents"  1-5, 8-15, 25
1623	32	7	4	3.27	2024-09-24 21:15:44.28	Генерация: "Reagents"  1-5, 8-15, 25
1624	32	7	5	33.67	2025-09-25 22:43:20.28	Генерация: "Reagents"  1-5, 8-15, 25
1625	32	6	3	-8.67	2025-02-25 03:18:58.28	Генерация: "Reagents"  1-5, 8-15, 25
1626	32	5	2	62.65	2024-06-17 03:17:14.28	Генерация: "Reagents"  1-5, 8-15, 25
1627	32	8	5	-8.54	2026-01-18 05:12:31.28	Генерация: "Reagents"  1-5, 8-15, 25
1628	32	5	3	51.92	2024-05-20 11:30:27.28	Генерация: "Reagents"  1-5, 8-15, 25
1629	32	5	2	10.60	2024-10-19 15:52:35.28	Генерация: "Reagents"  1-5, 8-15, 25
1630	32	6	3	43.68	2024-05-08 06:45:57.28	Генерация: "Reagents"  1-5, 8-15, 25
1631	32	7	5	-39.50	2024-05-02 23:30:54.28	Генерация: "Reagents"  1-5, 8-15, 25
1632	32	6	3	29.48	2025-05-25 23:18:28.28	Генерация: "Reagents"  1-5, 8-15, 25
1633	32	6	3	24.89	2024-04-24 02:26:48.28	Генерация: "Reagents"  1-5, 8-15, 25
1634	32	7	3	23.86	2024-08-20 08:37:04.28	Генерация: "Reagents"  1-5, 8-15, 25
1635	32	5	5	50.89	2024-12-04 05:29:39.28	Генерация: "Reagents"  1-5, 8-15, 25
1636	32	6	2	-49.60	2025-11-26 12:31:39.28	Генерация: "Reagents"  1-5, 8-15, 25
1637	32	7	3	38.28	2025-09-10 10:42:37.28	Генерация: "Reagents"  1-5, 8-15, 25
1638	32	5	4	57.44	2024-08-29 16:42:56.28	Генерация: "Reagents"  1-5, 8-15, 25
1639	32	6	4	3.69	2026-03-17 04:24:43.28	Генерация: "Reagents"  1-5, 8-15, 25
1640	32	8	2	-17.53	2025-03-26 23:06:48.28	Генерация: "Reagents"  1-5, 8-15, 25
1641	32	8	2	-6.83	2025-07-14 18:54:18.28	Генерация: "Reagents"  1-5, 8-15, 25
1642	32	5	4	59.39	2025-08-28 22:27:40.28	Генерация: "Reagents"  1-5, 8-15, 25
1643	32	8	3	95.99	2025-05-24 18:39:33.28	Генерация: "Reagents"  1-5, 8-15, 25
1644	32	8	5	93.86	2025-09-26 20:51:18.28	Генерация: "Reagents"  1-5, 8-15, 25
1645	32	6	5	37.98	2024-04-24 09:23:28.28	Генерация: "Reagents"  1-5, 8-15, 25
1646	32	5	3	-14.81	2025-03-06 09:52:22.28	Генерация: "Reagents"  1-5, 8-15, 25
1647	32	7	5	88.56	2025-04-03 13:04:42.28	Генерация: "Reagents"  1-5, 8-15, 25
1648	32	5	4	20.02	2024-10-16 14:20:12.28	Генерация: "Reagents"  1-5, 8-15, 25
1649	32	5	4	30.92	2026-01-14 11:53:52.28	Генерация: "Reagents"  1-5, 8-15, 25
1650	32	7	1	29.21	2025-10-12 12:29:49.28	Генерация: "Reagents"  1-5, 8-15, 25
1651	32	7	4	7.01	2025-09-01 22:28:13.28	Генерация: "Reagents"  1-5, 8-15, 25
1652	32	7	4	64.47	2025-05-28 09:40:39.28	Генерация: "Reagents"  1-5, 8-15, 25
1653	32	7	3	24.14	2025-07-23 09:44:59.28	Генерация: "Reagents"  1-5, 8-15, 25
1654	32	8	1	42.93	2025-02-16 20:15:40.28	Генерация: "Reagents"  1-5, 8-15, 25
1655	32	5	4	21.30	2025-12-31 06:03:27.28	Генерация: "Reagents"  1-5, 8-15, 25
1656	33	6	4	69.47	2024-07-26 13:29:00.64	Генерация: "Reagents"  1-5, 8-15, 25
1657	33	7	1	36.33	2024-04-12 00:14:39.64	Генерация: "Reagents"  1-5, 8-15, 25
1658	33	6	1	34.04	2024-05-26 22:26:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1659	33	5	3	65.64	2026-03-14 11:07:26.64	Генерация: "Reagents"  1-5, 8-15, 25
1660	33	7	5	-38.54	2025-11-07 19:56:52.64	Генерация: "Reagents"  1-5, 8-15, 25
1661	33	8	4	-24.41	2025-03-15 15:46:43.64	Генерация: "Reagents"  1-5, 8-15, 25
1662	33	6	5	-2.53	2026-02-08 02:28:27.64	Генерация: "Reagents"  1-5, 8-15, 25
1663	33	8	5	-7.96	2025-11-16 05:03:06.64	Генерация: "Reagents"  1-5, 8-15, 25
1664	33	7	1	53.40	2025-06-01 20:35:54.64	Генерация: "Reagents"  1-5, 8-15, 25
1665	33	6	3	-3.61	2025-12-03 01:44:53.64	Генерация: "Reagents"  1-5, 8-15, 25
1666	33	7	5	2.82	2026-01-16 20:21:29.64	Генерация: "Reagents"  1-5, 8-15, 25
1667	33	5	2	-36.32	2025-04-17 11:22:24.64	Генерация: "Reagents"  1-5, 8-15, 25
1668	33	7	1	29.49	2025-09-14 15:03:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1669	33	7	4	-44.61	2026-02-01 11:09:20.64	Генерация: "Reagents"  1-5, 8-15, 25
1670	33	7	3	62.51	2025-04-01 04:26:35.64	Генерация: "Reagents"  1-5, 8-15, 25
1671	33	6	1	78.05	2026-01-02 08:27:55.64	Генерация: "Reagents"  1-5, 8-15, 25
1672	33	8	4	65.70	2024-10-03 05:54:52.64	Генерация: "Reagents"  1-5, 8-15, 25
1673	33	6	5	24.75	2025-04-24 21:39:57.64	Генерация: "Reagents"  1-5, 8-15, 25
1674	33	6	4	-30.85	2025-04-15 16:13:33.64	Генерация: "Reagents"  1-5, 8-15, 25
1675	33	5	1	5.85	2025-01-03 21:44:22.64	Генерация: "Reagents"  1-5, 8-15, 25
1676	33	6	3	-10.20	2024-09-04 12:02:59.64	Генерация: "Reagents"  1-5, 8-15, 25
1677	33	6	2	22.58	2025-04-18 06:34:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1678	33	5	3	36.13	2025-10-12 07:47:18.64	Генерация: "Reagents"  1-5, 8-15, 25
1679	33	6	3	90.38	2024-06-03 12:11:32.64	Генерация: "Reagents"  1-5, 8-15, 25
1680	33	7	4	10.47	2025-02-11 13:38:46.64	Генерация: "Reagents"  1-5, 8-15, 25
1681	33	6	3	12.04	2025-10-16 01:51:46.64	Генерация: "Reagents"  1-5, 8-15, 25
1682	33	5	5	61.97	2024-09-10 01:19:13.64	Генерация: "Reagents"  1-5, 8-15, 25
1683	33	7	4	85.66	2026-01-28 22:01:10.64	Генерация: "Reagents"  1-5, 8-15, 25
1684	33	7	3	92.42	2024-06-26 14:27:22.64	Генерация: "Reagents"  1-5, 8-15, 25
1685	33	5	2	62.82	2025-03-04 10:49:07.64	Генерация: "Reagents"  1-5, 8-15, 25
1686	33	5	2	5.87	2025-01-14 06:27:37.64	Генерация: "Reagents"  1-5, 8-15, 25
1687	33	6	3	-42.64	2024-08-25 08:32:07.64	Генерация: "Reagents"  1-5, 8-15, 25
1688	33	8	4	-21.50	2025-06-14 17:11:57.64	Генерация: "Reagents"  1-5, 8-15, 25
1689	33	6	2	-12.67	2025-01-22 23:39:34.64	Генерация: "Reagents"  1-5, 8-15, 25
1690	33	5	5	50.82	2026-03-09 07:01:09.64	Генерация: "Reagents"  1-5, 8-15, 25
1691	33	7	3	-38.57	2025-03-24 07:33:01.64	Генерация: "Reagents"  1-5, 8-15, 25
1692	33	7	5	93.61	2026-02-21 13:54:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1693	33	6	3	99.38	2024-05-09 11:53:56.64	Генерация: "Reagents"  1-5, 8-15, 25
1694	33	5	4	72.89	2024-04-18 08:27:40.64	Генерация: "Reagents"  1-5, 8-15, 25
1695	33	5	1	82.46	2024-10-15 16:22:20.64	Генерация: "Reagents"  1-5, 8-15, 25
1696	33	7	2	62.29	2025-05-23 09:57:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1697	33	5	3	12.43	2025-03-07 05:57:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1698	33	5	2	-6.08	2026-03-07 22:33:26.64	Генерация: "Reagents"  1-5, 8-15, 25
1699	33	5	2	91.95	2024-04-20 23:58:58.64	Генерация: "Reagents"  1-5, 8-15, 25
1700	33	5	1	41.36	2025-09-16 19:40:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1701	33	7	2	41.05	2025-12-01 14:20:35.64	Генерация: "Reagents"  1-5, 8-15, 25
1702	33	8	2	0.18	2025-08-28 00:10:08.64	Генерация: "Reagents"  1-5, 8-15, 25
1703	33	6	4	35.07	2025-08-16 01:43:33.64	Генерация: "Reagents"  1-5, 8-15, 25
1704	33	8	3	6.94	2024-04-20 00:18:38.64	Генерация: "Reagents"  1-5, 8-15, 25
1705	33	7	1	69.40	2025-01-09 21:25:07.64	Генерация: "Reagents"  1-5, 8-15, 25
1706	33	8	2	89.23	2024-06-10 17:39:52.64	Генерация: "Reagents"  1-5, 8-15, 25
1707	33	6	2	32.76	2024-11-03 04:41:42.64	Генерация: "Reagents"  1-5, 8-15, 25
1708	33	8	3	34.37	2024-04-25 05:49:23.64	Генерация: "Reagents"  1-5, 8-15, 25
1709	33	8	3	56.79	2024-08-29 07:03:10.64	Генерация: "Reagents"  1-5, 8-15, 25
1710	33	6	4	-45.89	2024-10-20 01:06:04.64	Генерация: "Reagents"  1-5, 8-15, 25
1711	33	8	1	21.32	2024-10-21 18:40:09.64	Генерация: "Reagents"  1-5, 8-15, 25
1712	33	7	1	28.81	2025-12-16 04:58:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1713	33	6	2	23.38	2025-02-27 23:15:16.64	Генерация: "Reagents"  1-5, 8-15, 25
1714	33	5	5	26.77	2025-11-14 15:06:48.64	Генерация: "Reagents"  1-5, 8-15, 25
1715	33	6	2	2.39	2025-04-23 17:01:43.64	Генерация: "Reagents"  1-5, 8-15, 25
1716	33	8	1	53.91	2025-12-22 15:57:29.64	Генерация: "Reagents"  1-5, 8-15, 25
1717	33	8	2	87.96	2026-02-21 05:31:49.64	Генерация: "Reagents"  1-5, 8-15, 25
1718	33	5	1	88.67	2025-03-04 01:14:11.64	Генерация: "Reagents"  1-5, 8-15, 25
1719	33	5	4	-26.83	2024-12-27 06:02:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1720	33	6	4	-37.01	2025-06-18 13:59:18.64	Генерация: "Reagents"  1-5, 8-15, 25
1721	33	8	3	32.28	2025-08-12 10:27:39.64	Генерация: "Reagents"  1-5, 8-15, 25
1722	33	8	5	95.62	2025-04-12 04:19:19.64	Генерация: "Reagents"  1-5, 8-15, 25
1723	33	7	1	80.80	2025-07-22 17:04:30.64	Генерация: "Reagents"  1-5, 8-15, 25
1724	33	5	5	-16.77	2026-03-22 07:13:40.64	Генерация: "Reagents"  1-5, 8-15, 25
1725	33	5	4	85.04	2024-06-12 20:25:51.64	Генерация: "Reagents"  1-5, 8-15, 25
1726	33	6	4	-36.63	2025-11-24 05:02:03.64	Генерация: "Reagents"  1-5, 8-15, 25
1727	33	7	2	44.76	2025-02-19 08:47:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1728	33	7	1	95.71	2025-07-28 12:19:06.64	Генерация: "Reagents"  1-5, 8-15, 25
1729	33	8	4	-28.72	2025-12-05 23:42:37.64	Генерация: "Reagents"  1-5, 8-15, 25
1730	33	8	2	-43.71	2026-01-22 20:49:37.64	Генерация: "Reagents"  1-5, 8-15, 25
1731	33	8	3	-20.54	2026-02-22 17:11:15.64	Генерация: "Reagents"  1-5, 8-15, 25
1732	33	8	4	-16.04	2026-03-03 05:43:54.64	Генерация: "Reagents"  1-5, 8-15, 25
1733	33	7	4	-20.06	2025-10-27 07:02:39.64	Генерация: "Reagents"  1-5, 8-15, 25
1734	33	8	5	98.35	2024-11-05 11:03:58.64	Генерация: "Reagents"  1-5, 8-15, 25
1735	33	5	3	-1.19	2024-10-07 01:55:20.64	Генерация: "Reagents"  1-5, 8-15, 25
1736	33	5	1	49.31	2025-11-15 17:31:23.64	Генерация: "Reagents"  1-5, 8-15, 25
1737	33	5	3	-34.19	2024-12-02 18:26:27.64	Генерация: "Reagents"  1-5, 8-15, 25
1738	33	6	1	68.86	2025-12-08 04:48:42.64	Генерация: "Reagents"  1-5, 8-15, 25
1739	33	8	4	70.14	2025-07-11 11:19:08.64	Генерация: "Reagents"  1-5, 8-15, 25
1740	33	7	2	-42.84	2024-07-07 22:41:13.64	Генерация: "Reagents"  1-5, 8-15, 25
1741	33	6	5	0.94	2025-08-13 04:23:09.64	Генерация: "Reagents"  1-5, 8-15, 25
1742	33	6	5	83.19	2025-04-08 04:20:00.64	Генерация: "Reagents"  1-5, 8-15, 25
1743	33	6	2	23.15	2025-08-24 01:56:06.64	Генерация: "Reagents"  1-5, 8-15, 25
1744	33	7	3	-31.73	2026-04-06 04:18:27.64	Генерация: "Reagents"  1-5, 8-15, 25
1745	33	7	5	-9.81	2024-09-11 12:46:36.64	Генерация: "Reagents"  1-5, 8-15, 25
1746	33	7	5	-15.48	2025-01-07 19:18:17.64	Генерация: "Reagents"  1-5, 8-15, 25
1747	33	8	4	92.62	2024-08-12 18:44:29.64	Генерация: "Reagents"  1-5, 8-15, 25
1748	33	8	1	48.28	2024-11-24 01:09:52.64	Генерация: "Reagents"  1-5, 8-15, 25
1749	33	7	5	40.83	2024-04-27 22:37:23.64	Генерация: "Reagents"  1-5, 8-15, 25
1750	33	6	2	69.52	2024-05-24 17:37:50.64	Генерация: "Reagents"  1-5, 8-15, 25
1751	33	6	2	3.10	2025-04-10 09:00:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1752	33	7	2	97.06	2026-02-26 10:39:42.64	Генерация: "Reagents"  1-5, 8-15, 25
1753	33	8	5	-11.30	2024-04-20 23:13:59.64	Генерация: "Reagents"  1-5, 8-15, 25
1754	33	6	3	17.21	2025-09-16 06:50:17.64	Генерация: "Reagents"  1-5, 8-15, 25
1755	33	5	4	22.73	2025-03-08 18:30:20.64	Генерация: "Reagents"  1-5, 8-15, 25
1756	33	7	5	96.45	2024-05-03 16:24:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1757	33	8	5	70.67	2025-12-18 08:33:50.64	Генерация: "Reagents"  1-5, 8-15, 25
1758	33	5	4	4.87	2025-09-17 10:36:24.64	Генерация: "Reagents"  1-5, 8-15, 25
1759	33	8	5	46.51	2026-02-23 03:29:37.64	Генерация: "Reagents"  1-5, 8-15, 25
1760	33	6	1	33.67	2025-11-06 02:20:38.64	Генерация: "Reagents"  1-5, 8-15, 25
1761	33	6	3	82.96	2025-07-09 18:37:02.64	Генерация: "Reagents"  1-5, 8-15, 25
1762	33	8	4	39.21	2025-03-13 21:00:27.64	Генерация: "Reagents"  1-5, 8-15, 25
1763	33	5	5	-22.63	2025-08-07 17:35:48.64	Генерация: "Reagents"  1-5, 8-15, 25
1764	33	8	1	42.33	2025-02-17 04:38:00.64	Генерация: "Reagents"  1-5, 8-15, 25
1765	33	5	4	3.71	2024-07-01 14:24:10.64	Генерация: "Reagents"  1-5, 8-15, 25
1766	33	6	4	43.32	2024-12-10 03:02:29.64	Генерация: "Reagents"  1-5, 8-15, 25
1767	33	7	1	59.60	2025-05-13 23:42:17.64	Генерация: "Reagents"  1-5, 8-15, 25
1768	33	7	4	92.38	2024-12-14 10:45:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1769	33	7	3	-38.44	2026-02-25 11:59:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1770	33	6	5	68.21	2025-10-11 19:41:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1771	33	7	5	52.17	2025-06-14 18:12:04.64	Генерация: "Reagents"  1-5, 8-15, 25
1772	33	8	4	98.41	2025-01-13 06:04:39.64	Генерация: "Reagents"  1-5, 8-15, 25
1773	33	6	3	3.41	2025-10-02 02:53:09.64	Генерация: "Reagents"  1-5, 8-15, 25
1774	33	8	2	59.34	2025-04-18 15:15:25.64	Генерация: "Reagents"  1-5, 8-15, 25
1775	33	6	2	16.53	2024-08-27 06:00:00.64	Генерация: "Reagents"  1-5, 8-15, 25
1776	33	5	1	44.83	2024-05-20 09:06:46.64	Генерация: "Reagents"  1-5, 8-15, 25
1777	33	8	2	19.98	2024-09-18 10:56:04.64	Генерация: "Reagents"  1-5, 8-15, 25
1778	33	7	4	-3.69	2024-11-11 07:54:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1779	33	8	1	49.88	2024-07-10 07:27:01.64	Генерация: "Reagents"  1-5, 8-15, 25
1780	33	6	3	19.89	2025-06-15 08:49:22.64	Генерация: "Reagents"  1-5, 8-15, 25
1781	33	7	4	76.83	2025-07-08 18:22:17.64	Генерация: "Reagents"  1-5, 8-15, 25
1782	33	6	5	-34.28	2026-03-19 10:15:18.64	Генерация: "Reagents"  1-5, 8-15, 25
1783	33	6	3	42.99	2026-02-12 11:44:31.64	Генерация: "Reagents"  1-5, 8-15, 25
1784	33	6	5	10.68	2025-09-23 07:26:38.64	Генерация: "Reagents"  1-5, 8-15, 25
1785	33	7	1	96.49	2024-10-29 15:20:34.64	Генерация: "Reagents"  1-5, 8-15, 25
1786	33	7	5	-39.36	2025-01-20 20:25:40.64	Генерация: "Reagents"  1-5, 8-15, 25
1787	33	8	3	99.81	2025-01-05 18:03:27.64	Генерация: "Reagents"  1-5, 8-15, 25
1788	33	8	4	77.20	2025-01-24 06:35:21.64	Генерация: "Reagents"  1-5, 8-15, 25
1789	33	7	5	14.19	2025-07-31 01:38:24.64	Генерация: "Reagents"  1-5, 8-15, 25
1790	33	6	5	48.12	2024-09-04 04:17:22.64	Генерация: "Reagents"  1-5, 8-15, 25
1791	33	8	4	17.51	2025-05-26 17:12:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1792	33	8	4	-18.22	2025-02-17 03:40:30.64	Генерация: "Reagents"  1-5, 8-15, 25
1793	33	7	1	40.26	2024-04-17 03:08:18.64	Генерация: "Reagents"  1-5, 8-15, 25
1794	33	8	3	3.68	2025-12-20 05:34:18.64	Генерация: "Reagents"  1-5, 8-15, 25
1795	33	8	2	-22.04	2025-10-27 16:35:13.64	Генерация: "Reagents"  1-5, 8-15, 25
1796	33	8	1	35.62	2025-09-10 20:51:40.64	Генерация: "Reagents"  1-5, 8-15, 25
1797	33	8	2	2.09	2025-10-10 00:53:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1798	33	6	1	33.18	2026-02-19 07:13:10.64	Генерация: "Reagents"  1-5, 8-15, 25
1799	33	7	5	93.58	2025-12-12 10:24:19.64	Генерация: "Reagents"  1-5, 8-15, 25
1800	33	6	1	89.96	2024-12-21 12:45:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1801	33	6	5	26.20	2025-08-28 06:41:44.64	Генерация: "Reagents"  1-5, 8-15, 25
1802	33	8	3	3.67	2025-04-25 03:38:28.64	Генерация: "Reagents"  1-5, 8-15, 25
1803	33	7	4	64.13	2024-09-07 06:13:46.64	Генерация: "Reagents"  1-5, 8-15, 25
1804	33	7	2	44.49	2025-07-27 08:34:23.64	Генерация: "Reagents"  1-5, 8-15, 25
1805	33	5	3	51.58	2024-07-29 17:28:45.64	Генерация: "Reagents"  1-5, 8-15, 25
1806	34	5	2	8.20	2024-04-27 07:38:26.83	Генерация: "Reagents"  1-5, 8-15, 25
1807	34	8	5	7.08	2025-04-27 06:37:04.83	Генерация: "Reagents"  1-5, 8-15, 25
1808	34	6	3	87.82	2025-03-09 10:15:53.83	Генерация: "Reagents"  1-5, 8-15, 25
1809	34	5	2	97.67	2026-02-28 19:47:45.83	Генерация: "Reagents"  1-5, 8-15, 25
1810	34	5	4	65.35	2026-03-05 19:35:48.83	Генерация: "Reagents"  1-5, 8-15, 25
1811	34	8	3	41.18	2025-06-22 10:47:01.83	Генерация: "Reagents"  1-5, 8-15, 25
1812	34	7	5	-35.43	2024-04-29 20:32:59.83	Генерация: "Reagents"  1-5, 8-15, 25
1813	34	5	5	38.77	2025-05-24 09:18:26.83	Генерация: "Reagents"  1-5, 8-15, 25
1814	34	5	1	31.80	2025-03-22 05:00:09.83	Генерация: "Reagents"  1-5, 8-15, 25
1815	34	6	3	58.98	2025-02-27 17:41:30.83	Генерация: "Reagents"  1-5, 8-15, 25
1816	34	7	2	-5.24	2025-05-16 06:14:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1817	34	7	4	-3.87	2024-08-03 04:43:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1818	34	8	1	96.23	2025-06-03 01:04:08.83	Генерация: "Reagents"  1-5, 8-15, 25
1819	34	5	5	12.80	2024-05-29 10:51:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1820	34	8	4	-43.26	2024-10-25 17:46:27.83	Генерация: "Reagents"  1-5, 8-15, 25
1821	34	7	5	-32.39	2025-06-03 15:28:08.83	Генерация: "Reagents"  1-5, 8-15, 25
1822	34	5	3	59.61	2025-08-15 19:35:39.83	Генерация: "Reagents"  1-5, 8-15, 25
1823	34	5	5	57.90	2024-12-29 12:35:05.83	Генерация: "Reagents"  1-5, 8-15, 25
1824	34	8	4	-33.72	2025-12-05 21:28:38.83	Генерация: "Reagents"  1-5, 8-15, 25
1825	34	7	4	-20.97	2025-02-10 20:25:25.83	Генерация: "Reagents"  1-5, 8-15, 25
1826	34	5	3	55.68	2024-05-07 09:28:30.83	Генерация: "Reagents"  1-5, 8-15, 25
1827	34	8	4	6.31	2024-05-19 17:29:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1828	34	8	2	19.93	2024-11-11 12:58:58.83	Генерация: "Reagents"  1-5, 8-15, 25
1829	34	8	5	-22.48	2025-03-07 21:32:00.83	Генерация: "Reagents"  1-5, 8-15, 25
1830	34	5	2	40.96	2026-02-07 15:01:15.83	Генерация: "Reagents"  1-5, 8-15, 25
1831	34	5	1	26.33	2025-11-15 21:38:42.83	Генерация: "Reagents"  1-5, 8-15, 25
1832	34	5	5	45.05	2025-10-24 05:02:26.83	Генерация: "Reagents"  1-5, 8-15, 25
1833	34	8	3	74.28	2024-06-23 14:10:25.83	Генерация: "Reagents"  1-5, 8-15, 25
1834	34	5	2	-6.74	2025-01-16 16:35:39.83	Генерация: "Reagents"  1-5, 8-15, 25
1835	34	8	5	-48.24	2024-05-16 11:02:35.83	Генерация: "Reagents"  1-5, 8-15, 25
1836	34	8	2	-14.56	2025-06-02 05:59:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1837	34	6	4	77.15	2025-04-29 07:47:29.83	Генерация: "Reagents"  1-5, 8-15, 25
1838	34	6	1	33.37	2024-06-10 05:44:17.83	Генерация: "Reagents"  1-5, 8-15, 25
1839	34	5	4	31.58	2025-02-11 17:00:38.83	Генерация: "Reagents"  1-5, 8-15, 25
1840	34	6	4	74.65	2024-07-20 14:43:17.83	Генерация: "Reagents"  1-5, 8-15, 25
1841	34	7	2	46.37	2024-11-23 20:23:49.83	Генерация: "Reagents"  1-5, 8-15, 25
1842	34	8	4	7.42	2025-05-30 07:51:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1843	34	6	2	-19.46	2025-08-25 07:07:29.83	Генерация: "Reagents"  1-5, 8-15, 25
1844	34	8	4	94.23	2024-06-25 14:58:24.83	Генерация: "Reagents"  1-5, 8-15, 25
1845	34	5	5	-18.12	2025-01-10 22:14:16.83	Генерация: "Reagents"  1-5, 8-15, 25
1846	34	7	4	97.32	2024-08-02 16:25:43.83	Генерация: "Reagents"  1-5, 8-15, 25
1847	34	7	5	-47.62	2026-02-05 21:02:43.83	Генерация: "Reagents"  1-5, 8-15, 25
1848	34	5	5	-5.27	2024-11-05 04:36:25.83	Генерация: "Reagents"  1-5, 8-15, 25
1849	34	7	4	63.84	2025-07-24 00:17:54.83	Генерация: "Reagents"  1-5, 8-15, 25
1850	34	7	2	78.44	2025-11-25 05:35:11.83	Генерация: "Reagents"  1-5, 8-15, 25
1851	34	6	5	4.24	2024-08-23 12:57:13.83	Генерация: "Reagents"  1-5, 8-15, 25
1852	34	7	4	-2.74	2026-03-07 10:17:42.83	Генерация: "Reagents"  1-5, 8-15, 25
1853	34	7	4	-1.40	2024-08-19 11:22:29.83	Генерация: "Reagents"  1-5, 8-15, 25
1854	34	6	3	77.40	2026-02-07 08:59:22.83	Генерация: "Reagents"  1-5, 8-15, 25
1855	34	5	2	45.63	2024-08-06 01:18:35.83	Генерация: "Reagents"  1-5, 8-15, 25
1856	34	8	4	-24.09	2024-12-19 17:33:27.83	Генерация: "Reagents"  1-5, 8-15, 25
1857	34	6	2	83.87	2024-10-11 06:14:42.83	Генерация: "Reagents"  1-5, 8-15, 25
1858	34	6	5	25.27	2025-07-16 13:12:58.83	Генерация: "Reagents"  1-5, 8-15, 25
1859	34	8	3	-12.08	2025-09-23 13:47:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1860	34	6	1	45.70	2026-01-28 18:49:24.83	Генерация: "Reagents"  1-5, 8-15, 25
1861	34	7	1	64.74	2025-08-19 07:44:41.83	Генерация: "Reagents"  1-5, 8-15, 25
1862	34	6	5	-13.89	2024-11-28 14:54:20.83	Генерация: "Reagents"  1-5, 8-15, 25
1863	34	5	4	-32.36	2025-02-13 21:01:20.83	Генерация: "Reagents"  1-5, 8-15, 25
1864	34	5	2	-18.06	2024-11-13 11:33:07.83	Генерация: "Reagents"  1-5, 8-15, 25
1865	34	5	5	89.69	2024-08-28 05:10:52.83	Генерация: "Reagents"  1-5, 8-15, 25
1866	34	7	4	49.49	2024-07-18 17:35:54.83	Генерация: "Reagents"  1-5, 8-15, 25
1867	34	5	3	67.70	2024-12-20 11:07:10.83	Генерация: "Reagents"  1-5, 8-15, 25
1868	34	7	5	-40.66	2025-09-16 00:59:17.83	Генерация: "Reagents"  1-5, 8-15, 25
1869	34	5	1	85.02	2025-10-02 10:55:34.83	Генерация: "Reagents"  1-5, 8-15, 25
1870	34	5	4	80.18	2025-10-08 00:51:29.83	Генерация: "Reagents"  1-5, 8-15, 25
1871	34	6	3	14.51	2024-07-22 14:52:07.83	Генерация: "Reagents"  1-5, 8-15, 25
1872	34	8	2	19.36	2025-07-22 04:33:03.83	Генерация: "Reagents"  1-5, 8-15, 25
1873	34	8	3	62.76	2025-04-09 11:45:27.83	Генерация: "Reagents"  1-5, 8-15, 25
1874	34	7	3	-6.29	2025-12-19 11:18:30.83	Генерация: "Reagents"  1-5, 8-15, 25
1875	34	6	5	77.18	2025-04-09 00:26:41.83	Генерация: "Reagents"  1-5, 8-15, 25
1876	34	6	4	4.82	2025-11-01 04:24:00.83	Генерация: "Reagents"  1-5, 8-15, 25
1877	34	5	1	48.28	2025-03-01 22:50:29.83	Генерация: "Reagents"  1-5, 8-15, 25
1878	34	5	4	-18.53	2025-08-26 13:59:01.83	Генерация: "Reagents"  1-5, 8-15, 25
1879	34	6	5	-24.86	2025-08-08 15:28:16.83	Генерация: "Reagents"  1-5, 8-15, 25
1880	34	7	5	2.00	2024-09-03 18:38:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1881	34	7	5	-15.22	2025-04-07 17:29:38.83	Генерация: "Reagents"  1-5, 8-15, 25
1882	34	6	5	31.01	2026-01-28 22:52:34.83	Генерация: "Reagents"  1-5, 8-15, 25
1883	34	7	5	-38.04	2024-10-06 11:00:58.83	Генерация: "Reagents"  1-5, 8-15, 25
1884	34	6	1	27.48	2024-09-23 23:38:57.83	Генерация: "Reagents"  1-5, 8-15, 25
1885	34	7	2	-13.68	2025-10-28 21:25:46.83	Генерация: "Reagents"  1-5, 8-15, 25
1886	34	5	1	16.73	2025-07-19 10:34:08.83	Генерация: "Reagents"  1-5, 8-15, 25
1887	34	8	3	62.32	2025-11-12 02:12:39.83	Генерация: "Reagents"  1-5, 8-15, 25
1888	34	6	2	-25.06	2024-12-01 00:26:40.83	Генерация: "Reagents"  1-5, 8-15, 25
1889	34	8	2	51.59	2025-10-23 13:44:06.83	Генерация: "Reagents"  1-5, 8-15, 25
1890	34	6	1	31.10	2024-11-07 03:45:59.83	Генерация: "Reagents"  1-5, 8-15, 25
1891	34	6	4	-15.91	2025-01-05 06:07:19.83	Генерация: "Reagents"  1-5, 8-15, 25
1892	34	6	1	17.38	2024-06-18 21:57:32.83	Генерация: "Reagents"  1-5, 8-15, 25
1893	34	7	3	76.94	2026-03-13 14:21:26.83	Генерация: "Reagents"  1-5, 8-15, 25
1894	34	8	2	52.69	2025-06-12 17:01:48.83	Генерация: "Reagents"  1-5, 8-15, 25
1895	34	6	2	70.09	2024-12-06 17:34:23.83	Генерация: "Reagents"  1-5, 8-15, 25
1896	34	5	3	49.71	2024-12-20 05:37:10.83	Генерация: "Reagents"  1-5, 8-15, 25
1897	34	7	3	73.46	2025-11-22 06:12:51.83	Генерация: "Reagents"  1-5, 8-15, 25
1898	34	7	2	-16.70	2025-11-30 09:49:49.83	Генерация: "Reagents"  1-5, 8-15, 25
1899	34	6	4	-33.64	2024-06-08 21:14:55.83	Генерация: "Reagents"  1-5, 8-15, 25
1900	34	7	5	-30.19	2025-09-03 09:51:20.83	Генерация: "Reagents"  1-5, 8-15, 25
1901	34	8	5	13.92	2024-08-08 01:16:44.83	Генерация: "Reagents"  1-5, 8-15, 25
1902	34	5	4	31.86	2026-04-07 04:18:19.83	Генерация: "Reagents"  1-5, 8-15, 25
1903	34	7	5	47.64	2024-08-15 20:39:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1904	34	6	4	-3.53	2026-03-28 02:06:36.83	Генерация: "Reagents"  1-5, 8-15, 25
1905	34	5	5	-37.41	2025-04-26 11:54:44.83	Генерация: "Reagents"  1-5, 8-15, 25
1906	34	5	3	28.46	2024-08-07 05:50:57.83	Генерация: "Reagents"  1-5, 8-15, 25
1907	34	7	1	41.41	2024-07-08 02:43:35.83	Генерация: "Reagents"  1-5, 8-15, 25
1908	34	6	1	16.77	2024-07-24 18:35:14.83	Генерация: "Reagents"  1-5, 8-15, 25
1909	34	7	2	-48.36	2025-12-12 02:10:32.83	Генерация: "Reagents"  1-5, 8-15, 25
1910	34	8	2	-4.71	2024-12-21 15:01:22.83	Генерация: "Reagents"  1-5, 8-15, 25
1911	34	5	2	61.13	2024-04-14 05:10:05.83	Генерация: "Reagents"  1-5, 8-15, 25
1912	34	7	3	49.11	2025-02-16 21:38:03.83	Генерация: "Reagents"  1-5, 8-15, 25
1913	34	6	2	57.63	2026-03-22 14:11:53.83	Генерация: "Reagents"  1-5, 8-15, 25
1914	34	5	4	19.10	2025-03-29 20:06:58.83	Генерация: "Reagents"  1-5, 8-15, 25
1915	34	6	5	54.09	2025-07-06 09:17:28.83	Генерация: "Reagents"  1-5, 8-15, 25
1916	34	6	3	-45.18	2024-10-10 06:19:31.83	Генерация: "Reagents"  1-5, 8-15, 25
1917	34	6	3	38.97	2025-03-14 10:50:31.83	Генерация: "Reagents"  1-5, 8-15, 25
1918	34	8	1	19.55	2024-10-10 21:33:08.83	Генерация: "Reagents"  1-5, 8-15, 25
1919	34	5	5	91.07	2026-01-13 20:24:20.83	Генерация: "Reagents"  1-5, 8-15, 25
1920	34	8	5	65.93	2025-04-11 01:20:53.83	Генерация: "Reagents"  1-5, 8-15, 25
1921	34	6	3	-39.58	2025-11-02 21:13:32.83	Генерация: "Reagents"  1-5, 8-15, 25
1922	34	5	1	16.73	2025-09-03 01:55:51.83	Генерация: "Reagents"  1-5, 8-15, 25
1923	34	7	4	30.73	2024-05-07 17:21:09.83	Генерация: "Reagents"  1-5, 8-15, 25
1924	34	7	4	16.81	2025-12-31 17:07:06.83	Генерация: "Reagents"  1-5, 8-15, 25
1925	34	5	3	13.49	2024-04-09 14:04:50.83	Генерация: "Reagents"  1-5, 8-15, 25
1926	34	8	5	60.23	2025-03-03 14:48:07.83	Генерация: "Reagents"  1-5, 8-15, 25
1927	34	7	3	30.58	2025-12-04 00:35:58.83	Генерация: "Reagents"  1-5, 8-15, 25
1928	34	7	3	-28.66	2024-10-29 16:07:24.83	Генерация: "Reagents"  1-5, 8-15, 25
1929	34	5	2	-21.62	2025-03-19 07:32:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1930	34	7	2	30.80	2026-03-16 11:19:16.83	Генерация: "Reagents"  1-5, 8-15, 25
1931	34	6	5	80.72	2024-11-28 23:57:10.83	Генерация: "Reagents"  1-5, 8-15, 25
1932	34	6	4	-42.09	2024-09-08 18:00:22.83	Генерация: "Reagents"  1-5, 8-15, 25
1933	34	7	5	5.73	2025-11-24 21:57:07.83	Генерация: "Reagents"  1-5, 8-15, 25
1934	34	6	5	-24.23	2024-08-27 17:47:03.83	Генерация: "Reagents"  1-5, 8-15, 25
1935	34	6	4	-32.94	2025-02-09 00:33:03.83	Генерация: "Reagents"  1-5, 8-15, 25
1936	34	5	1	94.21	2025-08-04 03:34:43.83	Генерация: "Reagents"  1-5, 8-15, 25
1937	34	8	2	62.17	2024-05-14 01:59:44.83	Генерация: "Reagents"  1-5, 8-15, 25
1938	34	8	2	-48.16	2025-05-04 15:19:26.83	Генерация: "Reagents"  1-5, 8-15, 25
1939	34	6	2	61.39	2026-02-01 17:49:32.83	Генерация: "Reagents"  1-5, 8-15, 25
1940	34	5	3	83.63	2024-08-25 03:57:49.83	Генерация: "Reagents"  1-5, 8-15, 25
1941	34	6	5	35.62	2024-08-27 06:15:38.83	Генерация: "Reagents"  1-5, 8-15, 25
1942	34	6	1	43.34	2024-12-18 15:13:14.83	Генерация: "Reagents"  1-5, 8-15, 25
1943	34	6	1	63.83	2024-11-02 19:45:52.83	Генерация: "Reagents"  1-5, 8-15, 25
1944	34	6	2	98.51	2025-01-05 06:41:37.83	Генерация: "Reagents"  1-5, 8-15, 25
1945	34	6	4	91.09	2024-10-25 12:30:05.83	Генерация: "Reagents"  1-5, 8-15, 25
1946	34	7	2	26.13	2025-03-11 02:26:36.83	Генерация: "Reagents"  1-5, 8-15, 25
1947	34	7	5	-17.26	2025-06-29 03:32:56.83	Генерация: "Reagents"  1-5, 8-15, 25
1948	34	8	2	26.31	2026-02-16 20:18:59.83	Генерация: "Reagents"  1-5, 8-15, 25
1949	34	7	3	62.51	2024-06-29 20:11:48.83	Генерация: "Reagents"  1-5, 8-15, 25
1950	34	5	2	89.29	2025-05-19 15:21:22.83	Генерация: "Reagents"  1-5, 8-15, 25
1951	34	8	4	-34.00	2025-01-11 03:52:04.83	Генерация: "Reagents"  1-5, 8-15, 25
1952	34	7	5	94.76	2024-05-04 08:06:23.83	Генерация: "Reagents"  1-5, 8-15, 25
1953	34	8	4	83.04	2025-08-13 02:37:49.83	Генерация: "Reagents"  1-5, 8-15, 25
1954	34	8	3	94.43	2024-04-12 23:17:22.83	Генерация: "Reagents"  1-5, 8-15, 25
1955	34	5	4	21.08	2025-03-23 21:16:14.83	Генерация: "Reagents"  1-5, 8-15, 25
1956	35	5	2	90.25	2024-12-02 10:42:08.41	Генерация: "Reagents"  1-5, 8-15, 25
1957	35	6	4	66.50	2025-08-11 14:18:57.41	Генерация: "Reagents"  1-5, 8-15, 25
1958	35	7	2	76.48	2024-04-10 16:14:49.41	Генерация: "Reagents"  1-5, 8-15, 25
1959	35	6	3	54.54	2025-09-07 01:42:46.41	Генерация: "Reagents"  1-5, 8-15, 25
1960	35	8	3	95.53	2025-12-21 21:04:39.41	Генерация: "Reagents"  1-5, 8-15, 25
1961	35	8	5	-23.55	2024-09-20 21:17:36.41	Генерация: "Reagents"  1-5, 8-15, 25
1962	35	7	1	8.68	2025-02-20 17:26:50.41	Генерация: "Reagents"  1-5, 8-15, 25
1963	35	6	4	44.87	2025-08-15 05:28:12.41	Генерация: "Reagents"  1-5, 8-15, 25
1964	35	7	2	-24.47	2025-06-09 23:09:41.41	Генерация: "Reagents"  1-5, 8-15, 25
1965	35	6	1	90.01	2024-09-09 01:09:59.41	Генерация: "Reagents"  1-5, 8-15, 25
1966	35	8	5	74.51	2025-09-01 10:11:22.41	Генерация: "Reagents"  1-5, 8-15, 25
1967	35	8	5	-41.31	2024-04-27 14:46:50.41	Генерация: "Reagents"  1-5, 8-15, 25
1968	35	7	1	0.40	2025-06-10 04:03:38.41	Генерация: "Reagents"  1-5, 8-15, 25
1969	35	5	4	5.85	2025-11-25 22:42:43.41	Генерация: "Reagents"  1-5, 8-15, 25
1970	35	8	5	-21.64	2025-05-05 19:46:55.41	Генерация: "Reagents"  1-5, 8-15, 25
1971	35	7	1	95.88	2024-07-14 20:57:03.41	Генерация: "Reagents"  1-5, 8-15, 25
1972	35	5	4	-19.93	2025-01-17 07:29:36.41	Генерация: "Reagents"  1-5, 8-15, 25
1973	35	8	3	53.45	2024-06-06 17:56:03.41	Генерация: "Reagents"  1-5, 8-15, 25
1974	35	6	3	59.80	2024-05-27 17:37:03.41	Генерация: "Reagents"  1-5, 8-15, 25
1975	35	6	4	21.32	2024-06-13 11:48:18.41	Генерация: "Reagents"  1-5, 8-15, 25
1976	35	7	3	-17.25	2025-01-29 14:47:39.41	Генерация: "Reagents"  1-5, 8-15, 25
1977	35	6	4	-30.21	2024-08-04 17:09:54.41	Генерация: "Reagents"  1-5, 8-15, 25
1978	35	6	2	-38.54	2024-07-05 18:04:55.41	Генерация: "Reagents"  1-5, 8-15, 25
1979	35	5	4	-37.78	2025-08-16 07:12:16.41	Генерация: "Reagents"  1-5, 8-15, 25
1980	35	8	1	45.55	2024-07-25 18:25:58.41	Генерация: "Reagents"  1-5, 8-15, 25
1981	35	7	2	48.58	2025-07-29 00:15:22.41	Генерация: "Reagents"  1-5, 8-15, 25
1982	35	6	2	79.15	2026-01-10 17:06:39.41	Генерация: "Reagents"  1-5, 8-15, 25
1983	35	5	4	56.72	2024-08-04 14:05:33.41	Генерация: "Reagents"  1-5, 8-15, 25
1984	35	7	3	98.62	2024-05-24 19:45:28.41	Генерация: "Reagents"  1-5, 8-15, 25
1985	35	7	2	-6.80	2024-08-08 07:43:42.41	Генерация: "Reagents"  1-5, 8-15, 25
1986	35	8	1	42.06	2026-02-22 01:33:54.41	Генерация: "Reagents"  1-5, 8-15, 25
1987	35	5	2	13.74	2026-03-21 18:27:13.41	Генерация: "Reagents"  1-5, 8-15, 25
1988	35	7	2	64.64	2025-01-23 10:49:11.41	Генерация: "Reagents"  1-5, 8-15, 25
1989	35	7	4	-2.57	2025-04-05 06:35:59.41	Генерация: "Reagents"  1-5, 8-15, 25
1990	35	8	3	53.20	2024-11-23 09:44:34.41	Генерация: "Reagents"  1-5, 8-15, 25
1991	35	8	5	67.14	2024-05-06 07:42:50.41	Генерация: "Reagents"  1-5, 8-15, 25
1992	35	8	2	-22.76	2024-11-24 21:03:57.41	Генерация: "Reagents"  1-5, 8-15, 25
1993	35	8	5	4.41	2025-09-11 14:25:59.41	Генерация: "Reagents"  1-5, 8-15, 25
1994	35	7	3	83.06	2025-05-28 11:23:13.41	Генерация: "Reagents"  1-5, 8-15, 25
1995	35	8	5	35.86	2025-06-17 15:52:51.41	Генерация: "Reagents"  1-5, 8-15, 25
1996	35	8	4	69.06	2025-05-01 13:12:11.41	Генерация: "Reagents"  1-5, 8-15, 25
1997	35	7	2	87.66	2026-03-24 13:36:36.41	Генерация: "Reagents"  1-5, 8-15, 25
1998	35	5	5	-47.49	2024-10-27 08:27:00.41	Генерация: "Reagents"  1-5, 8-15, 25
1999	35	6	2	5.38	2024-07-24 05:48:56.41	Генерация: "Reagents"  1-5, 8-15, 25
2000	35	6	1	14.82	2025-08-25 12:30:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2001	35	6	1	13.48	2025-10-28 16:14:35.41	Генерация: "Reagents"  1-5, 8-15, 25
2002	35	6	1	27.08	2025-05-19 12:22:26.41	Генерация: "Reagents"  1-5, 8-15, 25
2003	35	7	4	25.95	2026-02-21 21:07:03.41	Генерация: "Reagents"  1-5, 8-15, 25
2004	35	6	2	27.50	2025-12-01 03:54:25.41	Генерация: "Reagents"  1-5, 8-15, 25
2005	35	6	3	-19.05	2025-04-05 19:27:06.41	Генерация: "Reagents"  1-5, 8-15, 25
2006	35	5	3	96.12	2025-10-01 14:13:43.41	Генерация: "Reagents"  1-5, 8-15, 25
2007	35	7	2	-3.08	2025-10-01 06:25:07.41	Генерация: "Reagents"  1-5, 8-15, 25
2008	35	5	2	75.03	2025-05-21 02:38:09.41	Генерация: "Reagents"  1-5, 8-15, 25
2009	35	8	2	-25.46	2025-12-09 14:10:39.41	Генерация: "Reagents"  1-5, 8-15, 25
2010	35	5	5	-46.17	2026-01-24 06:24:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2011	35	8	1	43.52	2025-07-16 00:25:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2012	35	8	3	72.20	2026-04-07 07:45:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2013	35	8	1	24.97	2024-11-19 22:13:33.41	Генерация: "Reagents"  1-5, 8-15, 25
2014	35	7	3	-40.73	2024-05-22 07:20:55.41	Генерация: "Reagents"  1-5, 8-15, 25
2015	35	5	1	82.90	2024-08-22 09:17:43.41	Генерация: "Reagents"  1-5, 8-15, 25
2016	35	5	2	58.45	2026-02-08 14:01:46.41	Генерация: "Reagents"  1-5, 8-15, 25
2017	35	7	3	1.82	2025-08-20 19:37:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2018	35	5	2	38.81	2025-10-10 04:12:37.41	Генерация: "Reagents"  1-5, 8-15, 25
2019	35	8	5	21.23	2025-08-28 04:54:41.41	Генерация: "Reagents"  1-5, 8-15, 25
2020	35	5	2	78.93	2025-09-14 13:54:11.41	Генерация: "Reagents"  1-5, 8-15, 25
2021	35	8	5	53.26	2024-10-07 10:32:50.41	Генерация: "Reagents"  1-5, 8-15, 25
2022	35	8	2	-19.62	2025-08-06 02:04:07.41	Генерация: "Reagents"  1-5, 8-15, 25
2023	35	6	5	28.94	2026-01-12 17:12:48.41	Генерация: "Reagents"  1-5, 8-15, 25
2024	35	5	2	-13.60	2025-09-04 10:41:45.41	Генерация: "Reagents"  1-5, 8-15, 25
2025	35	6	4	59.09	2024-12-24 01:11:58.41	Генерация: "Reagents"  1-5, 8-15, 25
2026	35	8	5	31.92	2025-02-26 06:20:56.41	Генерация: "Reagents"  1-5, 8-15, 25
2027	35	6	1	2.64	2025-12-07 08:00:14.41	Генерация: "Reagents"  1-5, 8-15, 25
2028	35	7	3	42.37	2025-05-07 08:43:48.41	Генерация: "Reagents"  1-5, 8-15, 25
2029	35	8	3	-29.01	2024-08-18 00:38:50.41	Генерация: "Reagents"  1-5, 8-15, 25
2030	35	8	2	-4.80	2024-08-03 10:51:16.41	Генерация: "Reagents"  1-5, 8-15, 25
2031	35	7	5	-22.39	2025-04-21 02:57:28.41	Генерация: "Reagents"  1-5, 8-15, 25
2032	35	5	4	21.01	2024-09-07 19:13:12.41	Генерация: "Reagents"  1-5, 8-15, 25
2033	35	6	1	78.71	2025-03-02 12:34:16.41	Генерация: "Reagents"  1-5, 8-15, 25
2034	35	7	5	-37.36	2025-07-26 03:04:45.41	Генерация: "Reagents"  1-5, 8-15, 25
2035	35	5	3	19.02	2025-09-12 21:53:02.41	Генерация: "Reagents"  1-5, 8-15, 25
2036	35	6	4	-32.76	2025-07-13 08:37:55.41	Генерация: "Reagents"  1-5, 8-15, 25
2037	35	6	3	84.62	2025-07-14 19:44:07.41	Генерация: "Reagents"  1-5, 8-15, 25
2038	35	8	1	39.62	2024-09-06 13:21:02.41	Генерация: "Reagents"  1-5, 8-15, 25
2039	35	5	1	2.79	2025-09-22 17:36:38.41	Генерация: "Reagents"  1-5, 8-15, 25
2040	35	7	5	88.56	2025-03-20 01:14:25.41	Генерация: "Reagents"  1-5, 8-15, 25
2041	35	5	4	47.30	2025-01-25 13:32:41.41	Генерация: "Reagents"  1-5, 8-15, 25
2042	35	5	1	59.66	2024-06-27 02:30:20.41	Генерация: "Reagents"  1-5, 8-15, 25
2043	35	6	4	76.00	2025-07-05 00:25:24.41	Генерация: "Reagents"  1-5, 8-15, 25
2044	35	8	2	49.61	2024-06-20 19:51:28.41	Генерация: "Reagents"  1-5, 8-15, 25
2045	35	6	5	51.35	2024-06-29 16:57:15.41	Генерация: "Reagents"  1-5, 8-15, 25
2046	35	6	4	74.97	2024-04-14 09:32:27.41	Генерация: "Reagents"  1-5, 8-15, 25
2047	35	7	4	0.35	2024-10-16 20:57:27.41	Генерация: "Reagents"  1-5, 8-15, 25
2048	35	6	2	-44.74	2024-12-06 10:18:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2049	35	6	2	11.93	2024-07-21 23:31:12.41	Генерация: "Reagents"  1-5, 8-15, 25
2050	35	6	4	83.39	2025-08-12 01:17:07.41	Генерация: "Reagents"  1-5, 8-15, 25
2051	35	7	2	17.51	2024-08-15 12:34:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2052	35	5	4	30.70	2024-11-22 20:56:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2053	35	5	5	-34.04	2024-12-07 08:19:55.41	Генерация: "Reagents"  1-5, 8-15, 25
2054	35	8	2	-17.91	2024-04-13 08:59:32.41	Генерация: "Reagents"  1-5, 8-15, 25
2055	35	8	4	-3.74	2025-03-08 18:44:58.41	Генерация: "Reagents"  1-5, 8-15, 25
2056	35	6	5	1.52	2024-12-11 06:51:17.41	Генерация: "Reagents"  1-5, 8-15, 25
2057	35	8	1	19.57	2025-01-01 02:08:34.41	Генерация: "Reagents"  1-5, 8-15, 25
2058	35	6	3	25.70	2024-10-26 05:43:43.41	Генерация: "Reagents"  1-5, 8-15, 25
2059	35	5	5	4.79	2025-02-20 20:32:13.41	Генерация: "Reagents"  1-5, 8-15, 25
2060	35	7	3	26.24	2024-08-17 21:57:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2061	35	6	2	-4.38	2025-09-28 06:22:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2062	35	7	1	3.53	2024-08-14 11:05:51.41	Генерация: "Reagents"  1-5, 8-15, 25
2063	35	5	1	97.76	2024-05-29 04:43:46.41	Генерация: "Reagents"  1-5, 8-15, 25
2064	35	7	1	22.39	2025-06-18 06:10:57.41	Генерация: "Reagents"  1-5, 8-15, 25
2065	35	8	3	67.67	2025-09-27 14:50:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2066	35	6	1	46.56	2025-08-04 21:55:51.41	Генерация: "Reagents"  1-5, 8-15, 25
2067	35	6	2	49.20	2024-12-10 07:53:10.41	Генерация: "Reagents"  1-5, 8-15, 25
2068	35	6	1	13.27	2025-07-04 02:02:47.41	Генерация: "Reagents"  1-5, 8-15, 25
2069	35	7	2	92.40	2026-01-06 08:56:03.41	Генерация: "Reagents"  1-5, 8-15, 25
2070	35	8	5	-7.47	2024-05-01 11:50:12.41	Генерация: "Reagents"  1-5, 8-15, 25
2071	35	6	5	89.04	2024-07-22 08:16:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2072	35	7	2	97.67	2026-01-11 03:09:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2073	35	8	1	20.28	2024-06-05 21:49:45.41	Генерация: "Reagents"  1-5, 8-15, 25
2074	35	7	5	-10.54	2026-02-26 12:15:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2075	35	7	5	-39.33	2025-01-15 15:30:14.41	Генерация: "Reagents"  1-5, 8-15, 25
2076	35	7	1	35.69	2025-08-25 18:40:55.41	Генерация: "Reagents"  1-5, 8-15, 25
2077	35	5	2	52.69	2024-07-23 18:56:50.41	Генерация: "Reagents"  1-5, 8-15, 25
2078	35	6	3	-38.79	2025-02-17 04:22:45.41	Генерация: "Reagents"  1-5, 8-15, 25
2079	35	7	3	-14.44	2024-06-07 04:03:06.41	Генерация: "Reagents"  1-5, 8-15, 25
2080	35	7	4	57.36	2025-02-07 10:49:09.41	Генерация: "Reagents"  1-5, 8-15, 25
2081	35	8	3	-21.34	2026-03-13 07:50:33.41	Генерация: "Reagents"  1-5, 8-15, 25
2082	35	7	2	53.87	2025-04-03 16:24:31.41	Генерация: "Reagents"  1-5, 8-15, 25
2083	35	8	4	21.09	2024-06-10 04:04:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2084	35	8	5	-18.54	2025-06-29 21:31:20.41	Генерация: "Reagents"  1-5, 8-15, 25
2085	35	8	5	53.43	2025-06-27 23:02:23.41	Генерация: "Reagents"  1-5, 8-15, 25
2086	35	5	4	-13.03	2024-10-31 16:33:25.41	Генерация: "Reagents"  1-5, 8-15, 25
2087	35	7	3	43.99	2025-03-26 21:04:49.41	Генерация: "Reagents"  1-5, 8-15, 25
2088	35	7	3	-28.95	2025-03-28 15:57:28.41	Генерация: "Reagents"  1-5, 8-15, 25
2089	35	7	3	51.85	2025-03-24 06:51:02.41	Генерация: "Reagents"  1-5, 8-15, 25
2090	35	5	3	0.00	2024-07-23 22:44:28.41	Генерация: "Reagents"  1-5, 8-15, 25
2091	35	8	3	8.23	2024-06-15 14:49:42.41	Генерация: "Reagents"  1-5, 8-15, 25
2092	35	8	2	-14.80	2025-01-14 22:43:35.41	Генерация: "Reagents"  1-5, 8-15, 25
2093	35	6	2	73.20	2025-08-23 17:50:11.41	Генерация: "Reagents"  1-5, 8-15, 25
2094	35	7	5	-40.99	2024-08-10 17:30:05.41	Генерация: "Reagents"  1-5, 8-15, 25
2095	35	7	3	-4.51	2025-02-07 04:22:00.41	Генерация: "Reagents"  1-5, 8-15, 25
2096	35	7	2	-1.89	2025-11-14 19:10:11.41	Генерация: "Reagents"  1-5, 8-15, 25
2097	35	7	4	72.55	2025-09-15 16:27:35.41	Генерация: "Reagents"  1-5, 8-15, 25
2098	35	8	1	34.91	2025-03-29 21:06:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2099	35	8	5	82.65	2025-06-04 19:35:54.41	Генерация: "Reagents"  1-5, 8-15, 25
2100	35	6	1	20.68	2025-10-08 09:33:39.41	Генерация: "Reagents"  1-5, 8-15, 25
2101	35	7	3	-37.57	2026-04-01 22:53:28.41	Генерация: "Reagents"  1-5, 8-15, 25
2102	35	7	4	-25.00	2024-11-24 14:31:59.41	Генерация: "Reagents"  1-5, 8-15, 25
2103	35	7	4	92.43	2024-06-05 02:38:34.41	Генерация: "Reagents"  1-5, 8-15, 25
2104	35	6	3	77.22	2024-11-03 13:55:52.41	Генерация: "Reagents"  1-5, 8-15, 25
2105	35	8	5	47.22	2024-07-03 14:03:38.41	Генерация: "Reagents"  1-5, 8-15, 25
2106	36	5	2	5.57	2025-12-13 20:47:55.33	Генерация: "Reagents"  1-5, 8-15, 25
2107	36	6	1	9.38	2025-07-11 02:04:37.33	Генерация: "Reagents"  1-5, 8-15, 25
2108	36	7	3	75.85	2025-06-06 13:34:39.33	Генерация: "Reagents"  1-5, 8-15, 25
2109	36	7	4	8.82	2024-12-03 02:06:55.33	Генерация: "Reagents"  1-5, 8-15, 25
2110	36	7	5	6.72	2025-08-17 14:24:04.33	Генерация: "Reagents"  1-5, 8-15, 25
2111	36	6	3	56.85	2024-05-13 17:05:30.33	Генерация: "Reagents"  1-5, 8-15, 25
2112	36	5	3	30.34	2026-04-07 04:26:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2113	36	5	3	-18.70	2025-03-26 18:18:30.33	Генерация: "Reagents"  1-5, 8-15, 25
2114	36	8	5	97.44	2024-10-24 09:58:24.33	Генерация: "Reagents"  1-5, 8-15, 25
2115	36	5	3	-24.36	2025-10-15 01:48:09.33	Генерация: "Reagents"  1-5, 8-15, 25
2116	36	5	2	-4.37	2024-07-03 09:53:04.33	Генерация: "Reagents"  1-5, 8-15, 25
2117	36	7	4	-49.32	2025-10-29 10:39:28.33	Генерация: "Reagents"  1-5, 8-15, 25
2118	36	8	3	-8.61	2024-09-30 15:53:17.33	Генерация: "Reagents"  1-5, 8-15, 25
2119	36	5	5	-24.53	2026-01-17 11:13:36.33	Генерация: "Reagents"  1-5, 8-15, 25
2120	36	7	2	79.65	2025-12-14 03:53:10.33	Генерация: "Reagents"  1-5, 8-15, 25
2121	36	6	3	-36.26	2025-07-09 06:34:21.33	Генерация: "Reagents"  1-5, 8-15, 25
2122	36	6	3	44.52	2025-09-15 17:07:03.33	Генерация: "Reagents"  1-5, 8-15, 25
2123	36	7	2	36.04	2025-11-11 17:38:09.33	Генерация: "Reagents"  1-5, 8-15, 25
2124	36	6	4	6.98	2025-08-24 04:35:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2125	36	6	3	-34.62	2025-08-28 18:07:33.33	Генерация: "Reagents"  1-5, 8-15, 25
2126	36	8	3	99.41	2025-03-23 09:42:41.33	Генерация: "Reagents"  1-5, 8-15, 25
2127	36	8	2	-32.68	2025-11-27 10:04:06.33	Генерация: "Reagents"  1-5, 8-15, 25
2128	36	8	4	51.64	2025-07-07 07:54:44.33	Генерация: "Reagents"  1-5, 8-15, 25
2129	36	8	5	-5.38	2025-05-11 01:50:21.33	Генерация: "Reagents"  1-5, 8-15, 25
2130	36	8	1	34.34	2025-06-22 22:25:07.33	Генерация: "Reagents"  1-5, 8-15, 25
2131	36	8	4	-0.34	2025-01-06 08:31:36.33	Генерация: "Reagents"  1-5, 8-15, 25
2132	36	7	3	22.33	2025-05-12 08:25:06.33	Генерация: "Reagents"  1-5, 8-15, 25
2133	36	7	4	27.19	2024-10-28 15:30:43.33	Генерация: "Reagents"  1-5, 8-15, 25
2134	36	7	1	33.22	2025-06-12 05:54:21.33	Генерация: "Reagents"  1-5, 8-15, 25
2135	36	6	3	25.55	2024-07-09 04:21:26.33	Генерация: "Reagents"  1-5, 8-15, 25
2136	36	7	1	24.51	2024-11-12 13:09:10.33	Генерация: "Reagents"  1-5, 8-15, 25
2137	36	5	1	16.93	2026-02-19 18:09:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2138	36	5	1	56.02	2025-08-27 17:35:04.33	Генерация: "Reagents"  1-5, 8-15, 25
2139	36	5	5	-26.72	2024-08-08 14:12:41.33	Генерация: "Reagents"  1-5, 8-15, 25
2140	36	7	3	93.47	2025-05-19 08:06:42.33	Генерация: "Reagents"  1-5, 8-15, 25
2141	36	6	3	81.48	2025-11-19 06:35:51.33	Генерация: "Reagents"  1-5, 8-15, 25
2142	36	8	5	11.26	2024-07-30 04:58:47.33	Генерация: "Reagents"  1-5, 8-15, 25
2143	36	8	1	16.04	2025-04-26 11:15:58.33	Генерация: "Reagents"  1-5, 8-15, 25
2144	36	5	1	26.09	2026-03-02 10:11:18.33	Генерация: "Reagents"  1-5, 8-15, 25
2145	36	5	2	0.69	2024-05-13 08:44:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2146	36	7	1	44.10	2025-04-28 03:49:06.33	Генерация: "Reagents"  1-5, 8-15, 25
2147	36	7	5	34.08	2025-11-24 08:31:31.33	Генерация: "Reagents"  1-5, 8-15, 25
2148	36	7	4	-42.92	2025-03-09 00:00:33.33	Генерация: "Reagents"  1-5, 8-15, 25
2149	36	6	2	-38.97	2026-01-30 20:20:32.33	Генерация: "Reagents"  1-5, 8-15, 25
2150	36	8	3	-30.35	2025-07-02 17:43:26.33	Генерация: "Reagents"  1-5, 8-15, 25
2151	36	7	5	87.77	2024-11-02 22:06:19.33	Генерация: "Reagents"  1-5, 8-15, 25
2152	36	6	3	8.66	2025-01-18 09:22:44.33	Генерация: "Reagents"  1-5, 8-15, 25
2153	36	7	4	17.23	2024-12-07 22:50:49.33	Генерация: "Reagents"  1-5, 8-15, 25
2154	36	7	3	14.33	2025-03-08 12:28:37.33	Генерация: "Reagents"  1-5, 8-15, 25
2155	36	5	2	28.60	2026-03-03 20:47:29.33	Генерация: "Reagents"  1-5, 8-15, 25
2156	36	8	1	27.55	2025-04-28 14:36:00.33	Генерация: "Reagents"  1-5, 8-15, 25
2157	36	8	4	-42.74	2025-12-29 08:13:22.33	Генерация: "Reagents"  1-5, 8-15, 25
2158	36	7	1	33.86	2025-11-06 07:06:58.33	Генерация: "Reagents"  1-5, 8-15, 25
2159	36	5	2	-35.65	2025-05-08 00:02:50.33	Генерация: "Reagents"  1-5, 8-15, 25
2160	36	7	1	52.54	2024-11-04 00:50:34.33	Генерация: "Reagents"  1-5, 8-15, 25
2161	36	7	3	48.34	2026-02-10 14:08:38.33	Генерация: "Reagents"  1-5, 8-15, 25
2162	36	7	5	92.02	2024-06-03 13:10:44.33	Генерация: "Reagents"  1-5, 8-15, 25
2163	36	5	3	-7.12	2024-08-18 08:07:45.33	Генерация: "Reagents"  1-5, 8-15, 25
2164	36	6	4	-24.36	2025-11-20 10:38:18.33	Генерация: "Reagents"  1-5, 8-15, 25
2165	36	6	5	3.71	2025-12-17 04:21:23.33	Генерация: "Reagents"  1-5, 8-15, 25
2166	36	8	5	25.26	2025-02-10 09:30:27.33	Генерация: "Reagents"  1-5, 8-15, 25
2167	36	6	4	84.85	2024-06-13 22:59:07.33	Генерация: "Reagents"  1-5, 8-15, 25
2168	36	7	4	-8.50	2026-03-14 05:52:16.33	Генерация: "Reagents"  1-5, 8-15, 25
2169	36	6	2	42.87	2025-10-01 06:24:31.33	Генерация: "Reagents"  1-5, 8-15, 25
2170	36	8	2	-39.36	2025-02-02 15:06:03.33	Генерация: "Reagents"  1-5, 8-15, 25
2171	36	8	2	-32.37	2024-09-17 14:04:29.33	Генерация: "Reagents"  1-5, 8-15, 25
2172	36	5	3	-21.09	2024-08-20 14:54:34.33	Генерация: "Reagents"  1-5, 8-15, 25
2173	36	8	5	42.76	2024-12-06 15:42:18.33	Генерация: "Reagents"  1-5, 8-15, 25
2174	36	8	3	73.61	2024-06-27 00:17:22.33	Генерация: "Reagents"  1-5, 8-15, 25
2175	36	8	5	-48.94	2024-11-15 00:19:28.33	Генерация: "Reagents"  1-5, 8-15, 25
2176	36	6	2	96.09	2024-05-14 07:06:25.33	Генерация: "Reagents"  1-5, 8-15, 25
2177	36	5	1	23.07	2025-05-05 23:52:42.33	Генерация: "Reagents"  1-5, 8-15, 25
2178	36	5	2	93.04	2025-04-12 17:11:52.33	Генерация: "Reagents"  1-5, 8-15, 25
2179	36	5	4	47.34	2025-07-24 12:18:24.33	Генерация: "Reagents"  1-5, 8-15, 25
2180	36	7	5	-44.94	2024-10-31 12:19:07.33	Генерация: "Reagents"  1-5, 8-15, 25
2181	36	6	2	24.12	2024-07-30 19:55:05.33	Генерация: "Reagents"  1-5, 8-15, 25
2182	36	5	3	-15.82	2025-04-15 18:26:02.33	Генерация: "Reagents"  1-5, 8-15, 25
2183	36	8	4	8.28	2025-07-23 06:10:32.33	Генерация: "Reagents"  1-5, 8-15, 25
2184	36	7	2	64.31	2026-03-28 10:31:23.33	Генерация: "Reagents"  1-5, 8-15, 25
2185	36	8	1	92.92	2024-12-14 09:49:31.33	Генерация: "Reagents"  1-5, 8-15, 25
2186	36	7	2	48.26	2025-03-12 15:54:17.33	Генерация: "Reagents"  1-5, 8-15, 25
2187	36	6	4	-14.69	2024-09-28 17:44:01.33	Генерация: "Reagents"  1-5, 8-15, 25
2188	36	5	2	38.10	2025-07-05 03:54:53.33	Генерация: "Reagents"  1-5, 8-15, 25
2189	36	8	1	70.04	2025-04-07 08:18:16.33	Генерация: "Reagents"  1-5, 8-15, 25
2190	36	8	1	17.35	2025-03-03 00:10:51.33	Генерация: "Reagents"  1-5, 8-15, 25
2191	36	5	2	45.27	2026-02-28 17:37:22.33	Генерация: "Reagents"  1-5, 8-15, 25
2192	36	7	3	-23.59	2025-05-29 23:26:32.33	Генерация: "Reagents"  1-5, 8-15, 25
2193	36	5	2	88.36	2024-10-16 07:31:35.33	Генерация: "Reagents"  1-5, 8-15, 25
2194	36	8	1	1.29	2025-03-07 19:32:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2195	36	6	3	64.73	2025-04-25 06:39:57.33	Генерация: "Reagents"  1-5, 8-15, 25
2196	36	5	5	-20.03	2026-04-05 17:22:48.33	Генерация: "Reagents"  1-5, 8-15, 25
2197	36	5	2	-31.86	2024-06-29 05:32:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2198	36	5	1	8.92	2024-05-05 02:10:40.33	Генерация: "Reagents"  1-5, 8-15, 25
2199	36	8	3	-28.65	2026-03-04 19:47:48.33	Генерация: "Reagents"  1-5, 8-15, 25
2200	36	5	4	-45.18	2024-05-31 03:28:16.33	Генерация: "Reagents"  1-5, 8-15, 25
2201	36	6	5	53.60	2024-11-26 15:35:27.33	Генерация: "Reagents"  1-5, 8-15, 25
2202	36	8	4	34.22	2024-08-25 15:15:28.33	Генерация: "Reagents"  1-5, 8-15, 25
2203	36	5	1	41.01	2026-03-13 15:27:52.33	Генерация: "Reagents"  1-5, 8-15, 25
2204	36	6	5	-32.54	2024-07-10 19:45:41.33	Генерация: "Reagents"  1-5, 8-15, 25
2205	36	5	1	7.12	2024-08-26 07:40:49.33	Генерация: "Reagents"  1-5, 8-15, 25
2206	36	6	1	33.83	2024-05-06 14:40:42.33	Генерация: "Reagents"  1-5, 8-15, 25
2207	36	6	2	7.60	2026-03-17 23:34:47.33	Генерация: "Reagents"  1-5, 8-15, 25
2208	36	8	3	74.12	2024-08-02 13:07:27.33	Генерация: "Reagents"  1-5, 8-15, 25
2209	36	5	3	91.92	2024-04-22 09:00:47.33	Генерация: "Reagents"  1-5, 8-15, 25
2210	36	7	1	25.01	2024-10-10 17:21:46.33	Генерация: "Reagents"  1-5, 8-15, 25
2211	36	8	2	78.54	2024-11-06 17:55:23.33	Генерация: "Reagents"  1-5, 8-15, 25
2212	36	5	1	30.16	2024-09-29 09:04:38.33	Генерация: "Reagents"  1-5, 8-15, 25
2213	36	5	2	58.82	2024-10-12 17:42:26.33	Генерация: "Reagents"  1-5, 8-15, 25
2214	36	8	1	38.71	2025-03-12 07:40:10.33	Генерация: "Reagents"  1-5, 8-15, 25
2215	36	8	4	37.91	2026-01-12 13:41:18.33	Генерация: "Reagents"  1-5, 8-15, 25
2216	36	6	2	66.06	2025-03-20 18:25:26.33	Генерация: "Reagents"  1-5, 8-15, 25
2217	36	8	1	47.72	2026-01-07 13:43:51.33	Генерация: "Reagents"  1-5, 8-15, 25
2218	36	8	5	40.90	2025-06-05 05:10:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2219	36	6	3	73.47	2025-03-27 01:18:47.33	Генерация: "Reagents"  1-5, 8-15, 25
2220	36	7	3	-21.16	2026-01-28 22:17:03.33	Генерация: "Reagents"  1-5, 8-15, 25
2221	36	5	3	-49.02	2024-04-27 11:56:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2222	36	6	5	40.16	2025-08-10 05:35:33.33	Генерация: "Reagents"  1-5, 8-15, 25
2223	36	8	5	46.96	2025-04-18 02:41:26.33	Генерация: "Reagents"  1-5, 8-15, 25
2224	36	8	4	47.75	2025-12-11 03:36:20.33	Генерация: "Reagents"  1-5, 8-15, 25
2225	36	7	1	37.69	2025-02-05 21:33:01.33	Генерация: "Reagents"  1-5, 8-15, 25
2226	36	8	5	64.75	2024-06-09 02:48:05.33	Генерация: "Reagents"  1-5, 8-15, 25
2227	36	5	1	37.28	2025-02-24 07:13:24.33	Генерация: "Reagents"  1-5, 8-15, 25
2228	36	7	3	3.31	2025-06-17 07:53:25.33	Генерация: "Reagents"  1-5, 8-15, 25
2229	36	7	2	41.65	2024-06-25 20:29:17.33	Генерация: "Reagents"  1-5, 8-15, 25
2230	36	8	2	-13.87	2025-01-21 09:59:40.33	Генерация: "Reagents"  1-5, 8-15, 25
2231	36	6	1	56.82	2025-06-16 09:00:50.33	Генерация: "Reagents"  1-5, 8-15, 25
2232	36	7	5	97.32	2025-01-23 22:45:35.33	Генерация: "Reagents"  1-5, 8-15, 25
2233	36	7	1	63.38	2024-09-09 03:21:29.33	Генерация: "Reagents"  1-5, 8-15, 25
2234	36	7	4	94.01	2026-03-30 05:30:23.33	Генерация: "Reagents"  1-5, 8-15, 25
2235	36	6	3	9.48	2026-01-29 04:42:37.33	Генерация: "Reagents"  1-5, 8-15, 25
2236	36	6	4	-12.86	2025-05-18 21:34:48.33	Генерация: "Reagents"  1-5, 8-15, 25
2237	36	8	3	74.64	2025-08-28 09:36:53.33	Генерация: "Reagents"  1-5, 8-15, 25
2238	36	8	1	34.90	2024-07-10 23:38:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2239	36	6	1	18.52	2025-06-12 06:50:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2240	36	8	5	67.51	2026-02-10 16:21:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2241	36	5	1	24.88	2026-01-17 11:02:50.33	Генерация: "Reagents"  1-5, 8-15, 25
2242	36	6	1	12.75	2026-01-16 04:05:58.33	Генерация: "Reagents"  1-5, 8-15, 25
2243	36	5	2	46.70	2025-05-14 16:23:12.33	Генерация: "Reagents"  1-5, 8-15, 25
2244	36	5	4	64.59	2024-11-05 08:05:56.33	Генерация: "Reagents"  1-5, 8-15, 25
2245	36	5	2	-10.72	2025-08-10 06:17:52.33	Генерация: "Reagents"  1-5, 8-15, 25
2246	36	8	3	29.66	2025-09-25 08:46:07.33	Генерация: "Reagents"  1-5, 8-15, 25
2247	36	7	5	19.81	2025-03-04 00:08:25.33	Генерация: "Reagents"  1-5, 8-15, 25
2248	36	8	2	9.65	2025-08-29 00:31:39.33	Генерация: "Reagents"  1-5, 8-15, 25
2249	36	6	2	35.40	2025-03-29 15:09:19.33	Генерация: "Reagents"  1-5, 8-15, 25
2250	36	6	5	77.03	2024-04-18 03:18:24.33	Генерация: "Reagents"  1-5, 8-15, 25
2251	36	5	1	0.08	2025-09-22 05:26:15.33	Генерация: "Reagents"  1-5, 8-15, 25
2252	36	5	3	-43.23	2024-05-23 14:25:18.33	Генерация: "Reagents"  1-5, 8-15, 25
2253	36	5	1	97.47	2025-09-01 22:46:45.33	Генерация: "Reagents"  1-5, 8-15, 25
2254	36	7	2	97.38	2025-07-15 04:08:45.33	Генерация: "Reagents"  1-5, 8-15, 25
2255	36	6	3	-46.99	2024-06-02 09:24:54.33	Генерация: "Reagents"  1-5, 8-15, 25
2256	37	5	5	-18.79	2025-03-21 08:31:47.39	Генерация: "Reagents"  1-5, 8-15, 25
2257	37	7	2	-37.40	2024-06-19 11:41:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2258	37	8	4	45.06	2026-01-20 06:22:33.39	Генерация: "Reagents"  1-5, 8-15, 25
2259	37	5	4	20.61	2024-07-16 15:00:25.39	Генерация: "Reagents"  1-5, 8-15, 25
2260	37	5	2	-45.36	2024-12-21 16:51:27.39	Генерация: "Reagents"  1-5, 8-15, 25
2261	37	8	1	43.58	2026-01-19 03:01:41.39	Генерация: "Reagents"  1-5, 8-15, 25
2262	37	6	5	65.24	2026-01-10 12:23:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2263	37	7	5	80.86	2024-07-22 22:13:58.39	Генерация: "Reagents"  1-5, 8-15, 25
2264	37	6	3	58.45	2025-01-13 03:22:49.39	Генерация: "Reagents"  1-5, 8-15, 25
2265	37	7	5	-25.29	2025-12-15 03:50:13.39	Генерация: "Reagents"  1-5, 8-15, 25
2266	37	6	5	88.64	2025-05-09 19:35:03.39	Генерация: "Reagents"  1-5, 8-15, 25
2267	37	6	3	9.95	2024-06-19 00:26:03.39	Генерация: "Reagents"  1-5, 8-15, 25
2268	37	8	4	26.99	2025-05-01 07:15:43.39	Генерация: "Reagents"  1-5, 8-15, 25
2269	37	7	2	16.95	2025-10-27 00:59:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2270	37	8	3	62.57	2026-01-09 12:14:51.39	Генерация: "Reagents"  1-5, 8-15, 25
2271	37	7	1	3.97	2025-03-08 16:10:40.39	Генерация: "Reagents"  1-5, 8-15, 25
2272	37	6	2	-28.84	2024-07-04 13:20:18.39	Генерация: "Reagents"  1-5, 8-15, 25
2273	37	5	2	67.98	2026-04-02 04:47:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2274	37	5	4	97.52	2025-12-21 06:57:33.39	Генерация: "Reagents"  1-5, 8-15, 25
2275	37	6	5	96.53	2024-08-05 22:11:21.39	Генерация: "Reagents"  1-5, 8-15, 25
2276	37	7	3	88.12	2025-01-29 00:05:29.39	Генерация: "Reagents"  1-5, 8-15, 25
2277	37	6	2	-46.05	2025-04-18 09:38:28.39	Генерация: "Reagents"  1-5, 8-15, 25
2278	37	5	5	52.01	2024-07-13 07:38:17.39	Генерация: "Reagents"  1-5, 8-15, 25
2279	37	7	4	64.37	2025-03-01 01:05:23.39	Генерация: "Reagents"  1-5, 8-15, 25
2280	37	5	1	16.26	2024-10-23 15:16:41.39	Генерация: "Reagents"  1-5, 8-15, 25
2281	37	8	4	-9.55	2024-08-09 09:02:42.39	Генерация: "Reagents"  1-5, 8-15, 25
2282	37	7	5	-27.30	2024-09-19 19:03:23.39	Генерация: "Reagents"  1-5, 8-15, 25
2283	37	8	2	92.49	2025-12-20 00:32:38.39	Генерация: "Reagents"  1-5, 8-15, 25
2284	37	8	5	68.01	2025-10-27 13:28:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2285	37	7	3	-43.83	2024-09-15 22:13:17.39	Генерация: "Reagents"  1-5, 8-15, 25
2286	37	8	4	78.72	2026-03-06 08:13:40.39	Генерация: "Reagents"  1-5, 8-15, 25
2287	37	6	2	50.49	2025-02-10 11:22:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2288	37	7	5	64.73	2025-02-01 09:34:04.39	Генерация: "Reagents"  1-5, 8-15, 25
2289	37	8	2	-27.37	2025-08-23 23:41:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2290	37	8	4	-32.16	2025-06-04 21:06:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2291	37	6	3	10.99	2024-11-07 16:12:33.39	Генерация: "Reagents"  1-5, 8-15, 25
2292	37	6	1	58.53	2025-05-02 11:55:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2293	37	8	4	28.85	2025-04-21 16:55:57.39	Генерация: "Reagents"  1-5, 8-15, 25
2294	37	7	5	-11.12	2024-08-14 14:46:01.39	Генерация: "Reagents"  1-5, 8-15, 25
2295	37	6	3	-25.14	2024-07-24 09:18:06.39	Генерация: "Reagents"  1-5, 8-15, 25
2296	37	7	1	37.59	2025-01-24 21:39:14.39	Генерация: "Reagents"  1-5, 8-15, 25
2297	37	7	2	-16.60	2025-09-12 01:06:15.39	Генерация: "Reagents"  1-5, 8-15, 25
2298	37	5	3	36.11	2025-05-11 13:08:18.39	Генерация: "Reagents"  1-5, 8-15, 25
2299	37	7	2	27.16	2025-04-10 02:37:48.39	Генерация: "Reagents"  1-5, 8-15, 25
2300	37	8	1	17.87	2026-02-14 20:19:12.39	Генерация: "Reagents"  1-5, 8-15, 25
2301	37	7	4	-3.01	2026-02-23 06:20:00.39	Генерация: "Reagents"  1-5, 8-15, 25
2302	37	6	5	-15.35	2025-12-27 11:41:56.39	Генерация: "Reagents"  1-5, 8-15, 25
2303	37	5	5	83.16	2025-06-30 04:35:43.39	Генерация: "Reagents"  1-5, 8-15, 25
2304	37	8	1	71.72	2025-09-24 09:32:36.39	Генерация: "Reagents"  1-5, 8-15, 25
2305	37	8	5	-29.37	2024-12-10 13:12:53.39	Генерация: "Reagents"  1-5, 8-15, 25
2306	37	6	1	93.41	2025-08-05 00:00:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2307	37	6	1	48.34	2025-10-07 01:09:34.39	Генерация: "Reagents"  1-5, 8-15, 25
2308	37	6	4	-31.28	2025-12-07 00:00:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2309	37	6	5	-20.14	2024-08-17 21:07:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2310	37	7	5	16.85	2025-07-12 19:02:23.39	Генерация: "Reagents"  1-5, 8-15, 25
2311	37	6	4	-20.11	2024-11-03 17:10:27.39	Генерация: "Reagents"  1-5, 8-15, 25
2312	37	7	2	59.56	2024-10-20 10:59:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2313	37	7	3	9.58	2025-10-07 07:08:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2314	37	7	2	48.48	2025-11-11 05:16:46.39	Генерация: "Reagents"  1-5, 8-15, 25
2315	37	5	1	2.30	2026-02-20 15:32:38.39	Генерация: "Reagents"  1-5, 8-15, 25
2316	37	8	3	79.67	2024-07-18 06:56:17.39	Генерация: "Reagents"  1-5, 8-15, 25
2317	37	5	4	21.61	2024-07-09 11:37:42.39	Генерация: "Reagents"  1-5, 8-15, 25
2318	37	7	5	-42.95	2026-01-21 09:52:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2319	37	5	5	66.85	2024-06-05 18:52:47.39	Генерация: "Reagents"  1-5, 8-15, 25
2320	37	8	3	51.55	2025-05-25 19:51:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2321	37	8	3	67.16	2024-09-23 09:38:13.39	Генерация: "Reagents"  1-5, 8-15, 25
2322	37	7	2	49.33	2024-05-26 17:47:24.39	Генерация: "Reagents"  1-5, 8-15, 25
2323	37	7	4	-31.23	2025-01-14 23:59:26.39	Генерация: "Reagents"  1-5, 8-15, 25
2324	37	6	4	-43.47	2024-05-15 05:52:03.39	Генерация: "Reagents"  1-5, 8-15, 25
2325	37	5	1	66.09	2025-12-01 10:57:20.39	Генерация: "Reagents"  1-5, 8-15, 25
2326	37	5	2	-0.40	2025-09-12 12:46:48.39	Генерация: "Reagents"  1-5, 8-15, 25
2327	37	8	5	97.12	2025-12-02 01:23:55.39	Генерация: "Reagents"  1-5, 8-15, 25
2328	37	5	5	93.42	2024-04-20 00:55:59.39	Генерация: "Reagents"  1-5, 8-15, 25
2329	37	5	3	36.73	2024-07-23 13:27:59.39	Генерация: "Reagents"  1-5, 8-15, 25
2330	37	6	2	93.96	2025-03-24 07:23:28.39	Генерация: "Reagents"  1-5, 8-15, 25
2331	37	8	5	99.34	2025-05-17 16:51:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2332	37	7	5	40.28	2024-05-29 23:04:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2333	37	5	3	-0.10	2025-01-14 00:33:38.39	Генерация: "Reagents"  1-5, 8-15, 25
2334	37	8	3	-2.55	2025-01-14 15:18:02.39	Генерация: "Reagents"  1-5, 8-15, 25
2335	37	5	2	6.60	2026-01-28 16:11:09.39	Генерация: "Reagents"  1-5, 8-15, 25
2336	37	5	2	-26.81	2025-04-11 22:10:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2337	37	5	1	6.90	2025-07-22 09:27:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2338	37	6	4	37.09	2024-10-12 02:50:36.39	Генерация: "Reagents"  1-5, 8-15, 25
2339	37	8	1	97.80	2024-10-06 12:21:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2340	37	7	3	87.98	2025-01-23 06:52:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2341	37	7	5	-28.67	2024-09-20 01:56:15.39	Генерация: "Reagents"  1-5, 8-15, 25
2342	37	7	4	78.92	2025-07-23 11:30:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2343	37	7	3	59.96	2025-08-10 21:22:43.39	Генерация: "Reagents"  1-5, 8-15, 25
2344	37	5	3	55.39	2025-05-20 16:01:51.39	Генерация: "Reagents"  1-5, 8-15, 25
2345	37	5	4	5.50	2025-12-29 20:16:41.39	Генерация: "Reagents"  1-5, 8-15, 25
2346	37	8	2	46.31	2025-10-13 04:07:13.39	Генерация: "Reagents"  1-5, 8-15, 25
2347	37	6	4	79.18	2025-06-02 12:54:17.39	Генерация: "Reagents"  1-5, 8-15, 25
2348	37	7	1	23.51	2024-12-11 14:12:22.39	Генерация: "Reagents"  1-5, 8-15, 25
2349	37	7	1	46.40	2024-09-06 06:07:04.39	Генерация: "Reagents"  1-5, 8-15, 25
2350	37	7	5	35.33	2025-09-30 17:51:54.39	Генерация: "Reagents"  1-5, 8-15, 25
2351	37	5	2	-39.26	2025-02-12 18:20:56.39	Генерация: "Reagents"  1-5, 8-15, 25
2352	37	8	5	-30.48	2025-08-09 02:13:41.39	Генерация: "Reagents"  1-5, 8-15, 25
2353	37	8	4	16.70	2025-04-12 00:48:16.39	Генерация: "Reagents"  1-5, 8-15, 25
2354	37	7	2	95.57	2025-11-19 00:08:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2355	37	8	4	93.71	2024-07-06 06:42:24.39	Генерация: "Reagents"  1-5, 8-15, 25
2356	37	7	5	-20.00	2024-05-21 12:45:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2357	37	6	5	15.33	2025-05-11 09:07:58.39	Генерация: "Reagents"  1-5, 8-15, 25
2358	37	8	3	93.51	2025-08-12 02:45:21.39	Генерация: "Reagents"  1-5, 8-15, 25
2359	37	6	3	45.71	2024-05-23 17:06:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2360	37	8	1	92.83	2025-03-17 02:48:54.39	Генерация: "Reagents"  1-5, 8-15, 25
2361	37	8	4	-46.59	2024-05-04 22:03:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2362	37	6	4	-27.92	2024-12-01 15:40:53.39	Генерация: "Reagents"  1-5, 8-15, 25
2363	37	6	2	-7.65	2025-05-26 02:48:32.39	Генерация: "Reagents"  1-5, 8-15, 25
2364	37	6	1	41.36	2025-04-13 18:08:36.39	Генерация: "Reagents"  1-5, 8-15, 25
2365	37	8	2	32.36	2025-11-17 12:47:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2366	37	6	2	6.09	2024-09-27 06:38:34.39	Генерация: "Reagents"  1-5, 8-15, 25
2367	37	8	4	76.15	2024-06-04 17:18:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2368	37	5	4	-1.13	2024-08-03 22:34:16.39	Генерация: "Reagents"  1-5, 8-15, 25
2369	37	7	1	60.47	2025-07-22 19:10:07.39	Генерация: "Reagents"  1-5, 8-15, 25
2370	37	6	1	18.11	2026-01-22 18:11:29.39	Генерация: "Reagents"  1-5, 8-15, 25
2371	37	5	2	26.56	2026-03-19 04:07:54.39	Генерация: "Reagents"  1-5, 8-15, 25
2372	37	5	2	70.75	2026-03-08 07:21:04.39	Генерация: "Reagents"  1-5, 8-15, 25
2373	37	5	5	74.60	2025-05-25 01:59:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2374	37	8	4	87.26	2024-04-09 02:58:30.39	Генерация: "Reagents"  1-5, 8-15, 25
2375	37	8	3	50.61	2025-10-12 21:37:38.39	Генерация: "Reagents"  1-5, 8-15, 25
2376	37	7	4	19.23	2024-12-03 08:36:04.39	Генерация: "Reagents"  1-5, 8-15, 25
2377	37	7	5	-20.76	2026-02-13 12:30:28.39	Генерация: "Reagents"  1-5, 8-15, 25
2378	37	5	3	-43.89	2025-07-14 06:27:55.39	Генерация: "Reagents"  1-5, 8-15, 25
2379	37	6	4	6.74	2024-11-10 04:23:05.39	Генерация: "Reagents"  1-5, 8-15, 25
2380	37	6	5	23.46	2025-06-11 16:55:09.39	Генерация: "Reagents"  1-5, 8-15, 25
2381	37	6	3	-34.85	2025-03-03 23:00:37.39	Генерация: "Reagents"  1-5, 8-15, 25
2382	37	5	3	88.24	2024-12-19 12:46:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2383	37	5	2	59.43	2024-07-15 20:26:34.39	Генерация: "Reagents"  1-5, 8-15, 25
2384	37	6	3	-12.12	2025-05-22 19:45:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2385	37	5	5	-5.37	2025-07-24 13:13:51.39	Генерация: "Reagents"  1-5, 8-15, 25
2386	37	7	3	41.59	2026-03-24 19:42:43.39	Генерация: "Reagents"  1-5, 8-15, 25
2387	37	7	5	59.51	2024-06-27 07:39:14.39	Генерация: "Reagents"  1-5, 8-15, 25
2388	37	8	4	-18.99	2025-02-23 10:00:08.39	Генерация: "Reagents"  1-5, 8-15, 25
2389	37	6	3	-44.90	2025-02-14 22:24:28.39	Генерация: "Reagents"  1-5, 8-15, 25
2390	37	7	3	-38.30	2026-01-23 01:53:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2391	37	5	5	63.59	2026-01-28 15:35:54.39	Генерация: "Reagents"  1-5, 8-15, 25
2392	37	8	2	2.68	2025-12-29 02:12:51.39	Генерация: "Reagents"  1-5, 8-15, 25
2393	37	5	5	51.43	2025-01-21 21:59:26.39	Генерация: "Reagents"  1-5, 8-15, 25
2394	37	6	1	8.99	2025-05-20 11:31:53.39	Генерация: "Reagents"  1-5, 8-15, 25
2395	37	6	3	-15.88	2024-07-18 07:32:57.39	Генерация: "Reagents"  1-5, 8-15, 25
2396	37	7	5	83.17	2026-03-28 12:53:24.39	Генерация: "Reagents"  1-5, 8-15, 25
2397	37	7	5	2.79	2024-07-25 06:24:47.39	Генерация: "Reagents"  1-5, 8-15, 25
2398	37	6	4	-47.07	2025-04-15 14:09:58.39	Генерация: "Reagents"  1-5, 8-15, 25
2399	37	8	1	69.89	2024-05-29 12:33:55.39	Генерация: "Reagents"  1-5, 8-15, 25
2400	37	6	4	-16.15	2025-10-09 05:44:47.39	Генерация: "Reagents"  1-5, 8-15, 25
2401	37	5	5	82.04	2025-05-02 14:40:52.39	Генерация: "Reagents"  1-5, 8-15, 25
2402	37	6	2	52.46	2025-05-20 08:40:10.39	Генерация: "Reagents"  1-5, 8-15, 25
2403	37	7	2	-19.69	2025-04-27 01:53:35.39	Генерация: "Reagents"  1-5, 8-15, 25
2404	37	8	3	99.30	2025-02-21 10:11:33.39	Генерация: "Reagents"  1-5, 8-15, 25
2405	37	8	4	25.54	2025-03-11 00:37:39.39	Генерация: "Reagents"  1-5, 8-15, 25
2406	38	6	1	14.94	2024-09-20 17:07:54.78	Генерация: "Reagents"  1-5, 8-15, 25
2407	38	5	3	87.13	2025-12-22 11:44:17.78	Генерация: "Reagents"  1-5, 8-15, 25
2408	38	6	2	56.56	2025-09-28 07:09:50.78	Генерация: "Reagents"  1-5, 8-15, 25
2409	38	8	3	98.98	2025-10-06 13:37:11.78	Генерация: "Reagents"  1-5, 8-15, 25
2410	38	6	4	-1.66	2024-05-06 03:59:38.78	Генерация: "Reagents"  1-5, 8-15, 25
2411	38	6	2	-9.71	2026-02-22 10:59:03.78	Генерация: "Reagents"  1-5, 8-15, 25
2412	38	7	5	83.47	2026-02-08 21:58:07.78	Генерация: "Reagents"  1-5, 8-15, 25
2413	38	5	4	-28.51	2025-06-17 06:01:35.78	Генерация: "Reagents"  1-5, 8-15, 25
2414	38	7	2	-8.49	2024-07-23 07:34:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2415	38	5	3	46.39	2024-11-30 13:25:21.78	Генерация: "Reagents"  1-5, 8-15, 25
2416	38	6	3	4.60	2025-08-31 03:40:36.78	Генерация: "Reagents"  1-5, 8-15, 25
2417	38	8	1	44.38	2024-07-21 06:37:05.78	Генерация: "Reagents"  1-5, 8-15, 25
2418	38	5	2	10.54	2025-11-08 05:32:38.78	Генерация: "Reagents"  1-5, 8-15, 25
2419	38	5	4	2.99	2025-03-08 08:30:11.78	Генерация: "Reagents"  1-5, 8-15, 25
2420	38	8	5	33.36	2026-01-12 06:01:04.78	Генерация: "Reagents"  1-5, 8-15, 25
2421	38	5	2	4.07	2024-10-14 21:32:38.78	Генерация: "Reagents"  1-5, 8-15, 25
2422	38	6	5	30.58	2025-12-26 18:50:54.78	Генерация: "Reagents"  1-5, 8-15, 25
2423	38	7	4	85.43	2025-03-05 16:30:50.78	Генерация: "Reagents"  1-5, 8-15, 25
2424	38	7	4	-15.89	2024-09-11 03:39:05.78	Генерация: "Reagents"  1-5, 8-15, 25
2425	38	7	5	63.04	2025-04-21 22:02:17.78	Генерация: "Reagents"  1-5, 8-15, 25
2426	38	6	1	60.27	2024-08-06 18:01:21.78	Генерация: "Reagents"  1-5, 8-15, 25
2427	38	7	2	53.81	2026-03-05 11:36:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2428	38	6	5	34.93	2024-06-19 13:35:08.78	Генерация: "Reagents"  1-5, 8-15, 25
2429	38	8	5	-31.14	2024-10-26 19:37:58.78	Генерация: "Reagents"  1-5, 8-15, 25
2430	38	5	1	60.50	2025-01-30 14:10:36.78	Генерация: "Reagents"  1-5, 8-15, 25
2431	38	8	3	64.39	2024-09-08 02:33:30.78	Генерация: "Reagents"  1-5, 8-15, 25
2432	38	6	5	-45.65	2024-04-13 04:03:43.78	Генерация: "Reagents"  1-5, 8-15, 25
2433	38	7	2	67.43	2024-10-10 16:41:05.78	Генерация: "Reagents"  1-5, 8-15, 25
2434	38	8	4	19.47	2024-08-08 09:27:35.78	Генерация: "Reagents"  1-5, 8-15, 25
2435	38	7	2	16.01	2025-03-06 01:16:45.78	Генерация: "Reagents"  1-5, 8-15, 25
2436	38	6	5	79.76	2025-05-21 16:07:08.78	Генерация: "Reagents"  1-5, 8-15, 25
2437	38	8	5	96.34	2025-04-19 04:30:01.78	Генерация: "Reagents"  1-5, 8-15, 25
2438	38	6	5	74.18	2024-09-15 01:03:20.78	Генерация: "Reagents"  1-5, 8-15, 25
2439	38	7	3	-0.78	2024-10-20 20:39:04.78	Генерация: "Reagents"  1-5, 8-15, 25
2440	38	5	4	-48.20	2024-08-10 10:53:30.78	Генерация: "Reagents"  1-5, 8-15, 25
2441	38	7	5	-28.81	2024-05-21 09:34:12.78	Генерация: "Reagents"  1-5, 8-15, 25
2442	38	6	4	79.10	2025-03-20 21:39:11.78	Генерация: "Reagents"  1-5, 8-15, 25
2443	38	7	1	8.94	2024-12-15 18:16:15.78	Генерация: "Reagents"  1-5, 8-15, 25
2444	38	7	1	71.87	2024-08-20 16:05:09.78	Генерация: "Reagents"  1-5, 8-15, 25
2445	38	8	3	95.29	2024-09-11 02:19:46.78	Генерация: "Reagents"  1-5, 8-15, 25
2446	38	7	2	99.85	2024-10-20 01:48:38.78	Генерация: "Reagents"  1-5, 8-15, 25
2447	38	7	4	-2.57	2024-10-04 08:15:54.78	Генерация: "Reagents"  1-5, 8-15, 25
2448	38	5	4	76.49	2025-12-06 11:25:34.78	Генерация: "Reagents"  1-5, 8-15, 25
2449	38	8	3	51.89	2026-03-07 18:50:58.78	Генерация: "Reagents"  1-5, 8-15, 25
2450	38	5	3	38.21	2026-01-01 03:06:53.78	Генерация: "Reagents"  1-5, 8-15, 25
2451	38	8	4	-42.34	2024-12-11 21:49:10.78	Генерация: "Reagents"  1-5, 8-15, 25
2452	38	6	3	94.88	2025-04-11 21:39:37.78	Генерация: "Reagents"  1-5, 8-15, 25
2453	38	8	1	95.87	2026-02-04 00:29:30.78	Генерация: "Reagents"  1-5, 8-15, 25
2454	38	8	4	63.64	2026-01-11 06:07:29.78	Генерация: "Reagents"  1-5, 8-15, 25
2455	38	8	1	18.49	2025-05-29 05:39:12.78	Генерация: "Reagents"  1-5, 8-15, 25
2456	38	6	4	-17.67	2024-08-07 00:32:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2457	38	6	3	-6.25	2025-02-14 23:08:39.78	Генерация: "Reagents"  1-5, 8-15, 25
2458	38	7	2	-35.82	2025-10-19 16:18:16.78	Генерация: "Reagents"  1-5, 8-15, 25
2459	38	5	4	94.60	2025-04-18 19:52:11.78	Генерация: "Reagents"  1-5, 8-15, 25
2460	38	6	5	-46.68	2025-07-17 17:17:39.78	Генерация: "Reagents"  1-5, 8-15, 25
2461	38	8	5	39.40	2025-09-20 17:05:09.78	Генерация: "Reagents"  1-5, 8-15, 25
2462	38	7	2	46.84	2024-10-14 08:20:43.78	Генерация: "Reagents"  1-5, 8-15, 25
2463	38	5	4	66.86	2024-08-19 19:58:57.78	Генерация: "Reagents"  1-5, 8-15, 25
2464	38	6	3	85.62	2026-02-02 23:35:32.78	Генерация: "Reagents"  1-5, 8-15, 25
2465	38	7	4	93.22	2025-02-07 11:21:24.78	Генерация: "Reagents"  1-5, 8-15, 25
2466	38	7	2	56.55	2025-07-22 00:38:23.78	Генерация: "Reagents"  1-5, 8-15, 25
2467	38	7	3	94.67	2025-07-01 09:54:16.78	Генерация: "Reagents"  1-5, 8-15, 25
2468	38	5	5	-31.33	2024-10-19 14:28:51.78	Генерация: "Reagents"  1-5, 8-15, 25
2469	38	7	5	82.77	2025-11-05 23:55:09.78	Генерация: "Reagents"  1-5, 8-15, 25
2470	38	6	4	-35.70	2025-06-15 19:42:05.78	Генерация: "Reagents"  1-5, 8-15, 25
2471	38	8	3	11.82	2025-10-30 08:41:32.78	Генерация: "Reagents"  1-5, 8-15, 25
2472	38	7	5	74.01	2024-07-16 14:46:34.78	Генерация: "Reagents"  1-5, 8-15, 25
2473	38	5	5	10.20	2025-04-05 12:52:09.78	Генерация: "Reagents"  1-5, 8-15, 25
2474	38	5	5	94.35	2026-03-11 22:39:42.78	Генерация: "Reagents"  1-5, 8-15, 25
2475	38	5	1	15.61	2025-02-19 21:41:05.78	Генерация: "Reagents"  1-5, 8-15, 25
2476	38	5	5	65.18	2026-01-25 12:32:28.78	Генерация: "Reagents"  1-5, 8-15, 25
2477	38	8	3	90.57	2024-10-08 10:07:12.78	Генерация: "Reagents"  1-5, 8-15, 25
2478	38	8	2	28.11	2024-04-22 01:47:25.78	Генерация: "Reagents"  1-5, 8-15, 25
2479	38	6	3	18.27	2025-11-24 21:42:20.78	Генерация: "Reagents"  1-5, 8-15, 25
2480	38	5	4	-44.53	2025-03-02 04:21:40.78	Генерация: "Reagents"  1-5, 8-15, 25
2481	38	8	5	-48.37	2025-02-11 21:58:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2482	38	6	3	68.69	2024-12-24 22:28:00.78	Генерация: "Reagents"  1-5, 8-15, 25
2483	38	6	5	39.31	2025-01-05 22:45:08.78	Генерация: "Reagents"  1-5, 8-15, 25
2484	38	6	1	15.27	2024-06-01 02:56:51.78	Генерация: "Reagents"  1-5, 8-15, 25
2485	38	8	3	92.20	2025-03-02 13:42:51.78	Генерация: "Reagents"  1-5, 8-15, 25
2486	38	7	4	22.31	2025-01-04 22:43:46.78	Генерация: "Reagents"  1-5, 8-15, 25
2487	38	6	4	-17.42	2025-08-24 04:27:00.78	Генерация: "Reagents"  1-5, 8-15, 25
2488	38	8	3	11.36	2026-02-17 12:36:18.78	Генерация: "Reagents"  1-5, 8-15, 25
2489	38	5	3	22.26	2025-04-30 04:00:28.78	Генерация: "Reagents"  1-5, 8-15, 25
2490	38	6	3	86.94	2025-01-27 20:51:54.78	Генерация: "Reagents"  1-5, 8-15, 25
2491	38	7	2	72.12	2024-09-19 03:18:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2492	38	7	3	-43.01	2026-01-31 07:47:19.78	Генерация: "Reagents"  1-5, 8-15, 25
2493	38	7	2	26.40	2024-12-17 14:53:34.78	Генерация: "Reagents"  1-5, 8-15, 25
2494	38	6	5	-0.63	2025-01-23 11:45:53.78	Генерация: "Reagents"  1-5, 8-15, 25
2495	38	6	2	62.48	2024-04-19 13:01:35.78	Генерация: "Reagents"  1-5, 8-15, 25
2496	38	7	5	-8.91	2025-03-16 17:20:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2497	38	6	5	53.02	2025-11-15 09:57:26.78	Генерация: "Reagents"  1-5, 8-15, 25
2498	38	8	1	18.43	2025-10-08 00:55:40.78	Генерация: "Reagents"  1-5, 8-15, 25
2499	38	7	3	-5.27	2025-11-29 22:38:06.78	Генерация: "Reagents"  1-5, 8-15, 25
2500	38	6	3	-26.66	2024-11-29 09:08:34.78	Генерация: "Reagents"  1-5, 8-15, 25
2501	38	5	4	45.58	2024-10-27 07:28:48.78	Генерация: "Reagents"  1-5, 8-15, 25
2502	38	8	2	43.33	2025-05-26 10:46:58.78	Генерация: "Reagents"  1-5, 8-15, 25
2503	38	8	5	32.88	2025-10-08 16:28:21.78	Генерация: "Reagents"  1-5, 8-15, 25
2504	38	5	1	53.86	2025-05-02 11:19:25.78	Генерация: "Reagents"  1-5, 8-15, 25
2505	38	6	2	-36.64	2025-03-28 16:52:07.78	Генерация: "Reagents"  1-5, 8-15, 25
2506	38	8	3	-19.30	2025-03-10 23:13:01.78	Генерация: "Reagents"  1-5, 8-15, 25
2507	38	8	5	-32.38	2024-07-18 20:05:20.78	Генерация: "Reagents"  1-5, 8-15, 25
2508	38	8	1	83.10	2024-10-19 19:21:57.78	Генерация: "Reagents"  1-5, 8-15, 25
2509	38	5	5	26.73	2025-12-16 04:17:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2510	38	8	1	98.14	2025-08-03 12:32:29.78	Генерация: "Reagents"  1-5, 8-15, 25
2511	38	8	2	77.87	2025-05-19 17:53:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2512	38	7	4	-43.60	2025-07-18 14:32:49.78	Генерация: "Reagents"  1-5, 8-15, 25
2513	38	8	3	0.72	2025-04-15 09:12:00.78	Генерация: "Reagents"  1-5, 8-15, 25
2514	38	6	5	6.56	2024-09-29 21:24:26.78	Генерация: "Reagents"  1-5, 8-15, 25
2515	38	6	4	53.64	2024-09-30 05:35:06.78	Генерация: "Reagents"  1-5, 8-15, 25
2516	38	7	3	60.60	2024-11-25 14:45:18.78	Генерация: "Reagents"  1-5, 8-15, 25
2517	38	7	3	-4.08	2024-08-28 22:07:36.78	Генерация: "Reagents"  1-5, 8-15, 25
2518	38	5	4	74.69	2025-11-15 11:12:51.78	Генерация: "Reagents"  1-5, 8-15, 25
2519	38	7	3	-5.67	2024-04-19 05:59:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2520	38	5	2	66.91	2025-09-23 02:29:07.78	Генерация: "Reagents"  1-5, 8-15, 25
2521	38	6	2	11.48	2025-05-28 22:39:21.78	Генерация: "Reagents"  1-5, 8-15, 25
2522	38	7	5	-19.58	2025-10-09 22:19:31.78	Генерация: "Reagents"  1-5, 8-15, 25
2523	38	5	3	0.82	2024-07-03 19:29:04.78	Генерация: "Reagents"  1-5, 8-15, 25
2524	38	8	4	-48.65	2025-06-19 21:54:08.78	Генерация: "Reagents"  1-5, 8-15, 25
2525	38	5	3	-3.36	2024-05-30 18:30:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2526	38	8	2	-4.37	2024-12-18 11:22:13.78	Генерация: "Reagents"  1-5, 8-15, 25
2527	38	7	5	0.63	2025-12-24 16:30:39.78	Генерация: "Reagents"  1-5, 8-15, 25
2528	38	6	4	98.40	2026-02-18 10:22:22.78	Генерация: "Reagents"  1-5, 8-15, 25
2529	38	8	4	36.04	2026-02-02 17:23:00.78	Генерация: "Reagents"  1-5, 8-15, 25
2530	38	6	2	-13.08	2025-11-29 20:10:37.78	Генерация: "Reagents"  1-5, 8-15, 25
2531	38	8	2	16.29	2025-09-26 11:44:29.78	Генерация: "Reagents"  1-5, 8-15, 25
2532	38	7	4	43.66	2025-01-03 05:50:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2533	38	8	5	25.72	2025-08-20 23:34:29.78	Генерация: "Reagents"  1-5, 8-15, 25
2534	38	8	2	13.35	2024-04-21 11:06:44.78	Генерация: "Reagents"  1-5, 8-15, 25
2535	38	8	1	99.81	2025-05-12 13:52:57.78	Генерация: "Reagents"  1-5, 8-15, 25
2536	38	7	1	30.20	2024-09-18 19:08:13.78	Генерация: "Reagents"  1-5, 8-15, 25
2537	38	6	1	71.44	2025-01-28 08:59:29.78	Генерация: "Reagents"  1-5, 8-15, 25
2538	38	6	3	8.92	2025-08-07 23:29:48.78	Генерация: "Reagents"  1-5, 8-15, 25
2539	38	8	1	22.29	2025-08-01 08:40:39.78	Генерация: "Reagents"  1-5, 8-15, 25
2540	38	7	2	6.05	2025-11-14 02:01:56.78	Генерация: "Reagents"  1-5, 8-15, 25
2541	38	6	4	7.84	2025-04-27 00:47:04.78	Генерация: "Reagents"  1-5, 8-15, 25
2542	38	5	5	55.77	2025-12-29 08:13:45.78	Генерация: "Reagents"  1-5, 8-15, 25
2543	38	8	1	11.60	2025-01-31 01:25:50.78	Генерация: "Reagents"  1-5, 8-15, 25
2544	38	8	2	-29.59	2025-10-23 12:29:41.78	Генерация: "Reagents"  1-5, 8-15, 25
2545	38	8	4	39.28	2024-09-02 09:48:56.78	Генерация: "Reagents"  1-5, 8-15, 25
2546	38	8	3	10.76	2025-07-24 23:39:46.78	Генерация: "Reagents"  1-5, 8-15, 25
2547	38	7	3	37.36	2024-10-31 20:44:04.78	Генерация: "Reagents"  1-5, 8-15, 25
2548	38	7	3	-44.77	2024-09-22 16:09:56.78	Генерация: "Reagents"  1-5, 8-15, 25
2549	38	7	4	27.69	2025-02-10 08:56:42.78	Генерация: "Reagents"  1-5, 8-15, 25
2550	38	7	5	67.10	2025-06-22 01:06:00.78	Генерация: "Reagents"  1-5, 8-15, 25
2551	38	8	4	32.62	2025-10-04 03:46:30.78	Генерация: "Reagents"  1-5, 8-15, 25
2552	38	8	3	68.68	2024-08-02 22:47:37.78	Генерация: "Reagents"  1-5, 8-15, 25
2553	38	5	3	35.03	2024-10-17 04:58:24.78	Генерация: "Reagents"  1-5, 8-15, 25
2554	38	7	1	94.66	2024-12-13 13:27:45.78	Генерация: "Reagents"  1-5, 8-15, 25
2555	38	5	1	99.71	2024-11-26 11:51:56.78	Генерация: "Reagents"  1-5, 8-15, 25
2556	39	5	2	-0.58	2025-01-10 21:01:17.85	Генерация: "Reagents"  1-5, 8-15, 25
2557	39	8	4	58.84	2026-01-11 02:43:24.85	Генерация: "Reagents"  1-5, 8-15, 25
2558	39	6	1	43.15	2024-09-09 01:20:47.85	Генерация: "Reagents"  1-5, 8-15, 25
2559	39	7	1	39.88	2024-04-11 08:42:16.85	Генерация: "Reagents"  1-5, 8-15, 25
2560	39	6	5	38.85	2024-10-03 20:48:59.85	Генерация: "Reagents"  1-5, 8-15, 25
2561	39	8	1	41.73	2025-07-15 20:53:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2562	39	7	4	19.12	2026-02-03 23:20:55.85	Генерация: "Reagents"  1-5, 8-15, 25
2563	39	7	4	24.87	2025-11-16 14:22:40.85	Генерация: "Reagents"  1-5, 8-15, 25
2564	39	5	4	78.58	2026-01-12 01:26:18.85	Генерация: "Reagents"  1-5, 8-15, 25
2565	39	5	1	29.00	2026-02-04 05:12:01.85	Генерация: "Reagents"  1-5, 8-15, 25
2566	39	5	4	-11.65	2025-05-02 23:03:21.85	Генерация: "Reagents"  1-5, 8-15, 25
2567	39	5	3	-18.23	2025-07-31 05:11:13.85	Генерация: "Reagents"  1-5, 8-15, 25
2568	39	7	4	-20.73	2024-04-30 16:22:27.85	Генерация: "Reagents"  1-5, 8-15, 25
2569	39	6	5	-14.05	2024-05-25 12:04:03.85	Генерация: "Reagents"  1-5, 8-15, 25
2570	39	6	4	30.59	2026-01-01 12:23:36.85	Генерация: "Reagents"  1-5, 8-15, 25
2571	39	5	5	11.15	2024-12-03 23:33:28.85	Генерация: "Reagents"  1-5, 8-15, 25
2572	39	8	3	-47.04	2024-06-30 02:16:39.85	Генерация: "Reagents"  1-5, 8-15, 25
2573	39	7	4	-35.15	2025-09-17 14:28:37.85	Генерация: "Reagents"  1-5, 8-15, 25
2574	39	5	5	69.42	2024-12-11 13:03:59.85	Генерация: "Reagents"  1-5, 8-15, 25
2575	39	6	1	0.15	2025-06-20 17:35:09.85	Генерация: "Reagents"  1-5, 8-15, 25
2576	39	6	3	-2.97	2024-05-26 20:27:49.85	Генерация: "Reagents"  1-5, 8-15, 25
2577	39	5	3	-1.80	2026-02-18 12:54:07.85	Генерация: "Reagents"  1-5, 8-15, 25
2578	39	8	3	17.72	2026-01-27 10:53:16.85	Генерация: "Reagents"  1-5, 8-15, 25
2579	39	5	4	52.76	2025-05-15 21:02:56.85	Генерация: "Reagents"  1-5, 8-15, 25
2580	39	7	2	86.27	2024-07-13 00:39:08.85	Генерация: "Reagents"  1-5, 8-15, 25
2581	39	7	2	-18.60	2025-12-02 01:54:45.85	Генерация: "Reagents"  1-5, 8-15, 25
2582	39	7	2	-31.35	2025-02-28 05:17:14.85	Генерация: "Reagents"  1-5, 8-15, 25
2583	39	5	5	-44.35	2024-11-11 15:33:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2584	39	7	3	32.36	2026-01-19 09:22:13.85	Генерация: "Reagents"  1-5, 8-15, 25
2585	39	7	5	28.52	2025-10-19 13:02:20.85	Генерация: "Reagents"  1-5, 8-15, 25
2586	39	8	1	93.93	2024-12-19 23:29:58.85	Генерация: "Reagents"  1-5, 8-15, 25
2587	39	7	3	75.95	2024-08-01 00:06:14.85	Генерация: "Reagents"  1-5, 8-15, 25
2588	39	6	5	41.80	2025-09-05 21:46:40.85	Генерация: "Reagents"  1-5, 8-15, 25
2589	39	5	5	96.83	2024-11-02 14:22:46.85	Генерация: "Reagents"  1-5, 8-15, 25
2590	39	8	5	27.60	2024-11-30 03:32:11.85	Генерация: "Reagents"  1-5, 8-15, 25
2591	39	6	4	5.53	2024-11-16 18:50:19.85	Генерация: "Reagents"  1-5, 8-15, 25
2592	39	8	2	-24.52	2024-12-10 08:43:59.85	Генерация: "Reagents"  1-5, 8-15, 25
2593	39	6	2	22.80	2025-09-21 05:42:10.85	Генерация: "Reagents"  1-5, 8-15, 25
2594	39	6	3	-5.21	2026-03-14 20:39:20.85	Генерация: "Reagents"  1-5, 8-15, 25
2595	39	5	3	-18.96	2024-12-05 23:37:35.85	Генерация: "Reagents"  1-5, 8-15, 25
2596	39	7	2	15.46	2024-05-05 16:16:44.85	Генерация: "Reagents"  1-5, 8-15, 25
2597	39	7	3	59.78	2024-09-22 05:36:02.85	Генерация: "Reagents"  1-5, 8-15, 25
2598	39	8	1	17.58	2025-01-02 21:45:28.85	Генерация: "Reagents"  1-5, 8-15, 25
2599	39	6	1	84.25	2024-12-20 06:58:13.85	Генерация: "Reagents"  1-5, 8-15, 25
2600	39	8	1	12.98	2024-05-04 22:30:21.85	Генерация: "Reagents"  1-5, 8-15, 25
2601	39	8	3	-21.94	2024-07-12 09:34:17.85	Генерация: "Reagents"  1-5, 8-15, 25
2602	39	8	5	5.48	2025-08-31 16:58:47.85	Генерация: "Reagents"  1-5, 8-15, 25
2603	39	6	2	-46.80	2025-12-29 07:01:26.85	Генерация: "Reagents"  1-5, 8-15, 25
2604	39	8	2	-31.21	2025-08-29 14:57:17.85	Генерация: "Reagents"  1-5, 8-15, 25
2605	39	5	1	72.89	2024-09-01 23:12:35.85	Генерация: "Reagents"  1-5, 8-15, 25
2606	39	5	5	35.69	2024-12-17 19:00:28.85	Генерация: "Reagents"  1-5, 8-15, 25
2607	39	5	2	-45.33	2025-06-21 15:17:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2608	39	7	3	-9.24	2025-10-29 22:12:02.85	Генерация: "Reagents"  1-5, 8-15, 25
2609	39	6	4	83.21	2024-07-21 12:29:21.85	Генерация: "Reagents"  1-5, 8-15, 25
2610	39	8	2	11.44	2025-06-13 21:02:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2611	39	6	3	-2.33	2024-09-29 17:44:37.85	Генерация: "Reagents"  1-5, 8-15, 25
2612	39	8	3	85.50	2024-08-27 03:17:23.85	Генерация: "Reagents"  1-5, 8-15, 25
2613	39	8	4	64.59	2025-07-05 02:26:39.85	Генерация: "Reagents"  1-5, 8-15, 25
2614	39	6	2	25.65	2024-08-05 08:46:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2615	39	5	3	7.42	2024-04-09 11:51:27.85	Генерация: "Reagents"  1-5, 8-15, 25
2616	39	7	1	74.77	2025-09-18 18:13:27.85	Генерация: "Reagents"  1-5, 8-15, 25
2617	39	8	5	2.73	2025-10-02 22:09:31.85	Генерация: "Reagents"  1-5, 8-15, 25
2618	39	6	5	-2.85	2024-11-09 22:54:00.85	Генерация: "Reagents"  1-5, 8-15, 25
2619	39	5	3	-10.18	2025-08-19 12:42:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2620	39	7	3	-0.08	2024-12-10 11:39:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2621	39	7	5	25.17	2026-02-07 19:34:47.85	Генерация: "Reagents"  1-5, 8-15, 25
2622	39	8	4	19.66	2025-01-11 08:31:57.85	Генерация: "Reagents"  1-5, 8-15, 25
2623	39	7	2	92.24	2025-05-23 20:31:08.85	Генерация: "Reagents"  1-5, 8-15, 25
2624	39	8	5	87.96	2025-04-05 09:08:26.85	Генерация: "Reagents"  1-5, 8-15, 25
2625	39	5	2	99.28	2025-02-15 05:16:20.85	Генерация: "Reagents"  1-5, 8-15, 25
2626	39	5	4	91.86	2025-02-02 15:38:01.85	Генерация: "Reagents"  1-5, 8-15, 25
2627	39	8	1	29.76	2025-10-19 08:59:51.85	Генерация: "Reagents"  1-5, 8-15, 25
2628	39	6	2	73.79	2026-03-17 18:29:22.85	Генерация: "Reagents"  1-5, 8-15, 25
2629	39	6	2	10.74	2024-09-14 23:30:50.85	Генерация: "Reagents"  1-5, 8-15, 25
2630	39	7	1	26.20	2025-10-01 00:40:46.85	Генерация: "Reagents"  1-5, 8-15, 25
2631	39	6	1	43.45	2025-01-03 00:25:00.85	Генерация: "Reagents"  1-5, 8-15, 25
2632	39	7	3	78.97	2024-08-13 13:28:10.85	Генерация: "Reagents"  1-5, 8-15, 25
2633	39	7	1	20.03	2024-10-11 19:53:11.85	Генерация: "Reagents"  1-5, 8-15, 25
2634	39	6	1	50.15	2025-11-01 22:32:17.85	Генерация: "Reagents"  1-5, 8-15, 25
2635	39	7	3	82.01	2025-08-06 17:47:37.85	Генерация: "Reagents"  1-5, 8-15, 25
2636	39	8	1	9.46	2024-10-12 05:11:59.85	Генерация: "Reagents"  1-5, 8-15, 25
2637	39	5	1	8.56	2024-07-29 21:28:31.85	Генерация: "Reagents"  1-5, 8-15, 25
2638	39	8	1	92.79	2025-05-12 17:19:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2639	39	7	4	-9.99	2025-01-09 12:43:19.85	Генерация: "Reagents"  1-5, 8-15, 25
2640	39	7	2	-43.50	2025-08-24 16:44:47.85	Генерация: "Reagents"  1-5, 8-15, 25
2641	39	6	1	28.33	2025-02-26 14:17:35.85	Генерация: "Reagents"  1-5, 8-15, 25
2642	39	5	2	82.58	2025-12-18 14:30:45.85	Генерация: "Reagents"  1-5, 8-15, 25
2643	39	6	4	53.21	2024-08-21 09:57:51.85	Генерация: "Reagents"  1-5, 8-15, 25
2644	39	5	4	-28.08	2025-11-05 23:37:16.85	Генерация: "Reagents"  1-5, 8-15, 25
2645	39	8	5	-22.48	2025-07-08 20:37:40.85	Генерация: "Reagents"  1-5, 8-15, 25
2646	39	8	5	37.54	2025-10-09 03:39:57.85	Генерация: "Reagents"  1-5, 8-15, 25
2647	39	5	3	-4.71	2026-01-08 08:03:21.85	Генерация: "Reagents"  1-5, 8-15, 25
2648	39	7	1	52.22	2025-03-09 07:32:20.85	Генерация: "Reagents"  1-5, 8-15, 25
2649	39	7	4	96.89	2025-11-20 08:17:40.85	Генерация: "Reagents"  1-5, 8-15, 25
2650	39	8	2	-14.62	2025-09-28 02:24:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2651	39	7	5	35.23	2026-01-21 00:18:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2652	39	8	2	-7.86	2025-02-15 06:20:13.85	Генерация: "Reagents"  1-5, 8-15, 25
2653	39	6	3	-11.97	2025-09-23 23:34:29.85	Генерация: "Reagents"  1-5, 8-15, 25
2654	39	6	2	88.90	2026-01-30 21:08:52.85	Генерация: "Reagents"  1-5, 8-15, 25
2655	39	7	1	58.97	2025-12-12 15:27:56.85	Генерация: "Reagents"  1-5, 8-15, 25
2656	39	6	2	84.14	2024-05-05 18:26:56.85	Генерация: "Reagents"  1-5, 8-15, 25
2657	39	5	5	48.20	2025-11-30 03:01:50.85	Генерация: "Reagents"  1-5, 8-15, 25
2658	39	7	5	-30.69	2024-10-24 11:26:18.85	Генерация: "Reagents"  1-5, 8-15, 25
2659	39	8	3	62.73	2025-04-04 06:02:21.85	Генерация: "Reagents"  1-5, 8-15, 25
2660	39	7	1	22.65	2025-06-28 06:45:41.85	Генерация: "Reagents"  1-5, 8-15, 25
2661	39	7	2	72.28	2025-12-18 19:32:12.85	Генерация: "Reagents"  1-5, 8-15, 25
2662	39	5	4	6.94	2025-01-23 20:45:43.85	Генерация: "Reagents"  1-5, 8-15, 25
2663	39	6	2	37.77	2025-08-31 13:35:07.85	Генерация: "Reagents"  1-5, 8-15, 25
2664	39	5	2	20.91	2025-01-02 18:35:01.85	Генерация: "Reagents"  1-5, 8-15, 25
2665	39	6	4	-23.21	2024-09-24 10:40:22.85	Генерация: "Reagents"  1-5, 8-15, 25
2666	39	8	2	-34.17	2024-11-26 16:50:19.85	Генерация: "Reagents"  1-5, 8-15, 25
2667	39	8	1	45.19	2025-01-04 03:58:18.85	Генерация: "Reagents"  1-5, 8-15, 25
2668	39	8	3	49.77	2025-05-29 07:40:29.85	Генерация: "Reagents"  1-5, 8-15, 25
2669	39	7	5	31.58	2025-10-21 20:38:43.85	Генерация: "Reagents"  1-5, 8-15, 25
2670	39	6	5	-29.96	2025-08-27 04:50:54.85	Генерация: "Reagents"  1-5, 8-15, 25
2671	39	8	5	1.52	2025-11-22 00:21:08.85	Генерация: "Reagents"  1-5, 8-15, 25
2672	39	8	3	22.91	2025-10-31 02:42:18.85	Генерация: "Reagents"  1-5, 8-15, 25
2673	39	5	5	51.31	2025-03-21 10:23:12.85	Генерация: "Reagents"  1-5, 8-15, 25
2674	39	8	3	86.72	2025-06-02 19:40:58.85	Генерация: "Reagents"  1-5, 8-15, 25
2675	39	8	2	18.35	2024-10-31 15:38:08.85	Генерация: "Reagents"  1-5, 8-15, 25
2676	39	7	2	-20.76	2025-08-16 22:43:02.85	Генерация: "Reagents"  1-5, 8-15, 25
2677	39	6	4	-19.33	2025-04-13 04:22:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2678	39	6	2	10.59	2024-07-05 21:17:26.85	Генерация: "Reagents"  1-5, 8-15, 25
2679	39	5	2	75.69	2025-12-13 10:26:39.85	Генерация: "Reagents"  1-5, 8-15, 25
2680	39	6	1	78.80	2025-04-24 02:32:08.85	Генерация: "Reagents"  1-5, 8-15, 25
2681	39	6	2	41.44	2025-12-28 21:27:11.85	Генерация: "Reagents"  1-5, 8-15, 25
2682	39	8	5	45.42	2025-06-13 15:38:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2683	39	7	1	97.60	2024-05-23 02:22:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2684	39	5	3	65.43	2025-06-08 21:42:25.85	Генерация: "Reagents"  1-5, 8-15, 25
2685	39	6	4	67.66	2025-05-31 13:08:51.85	Генерация: "Reagents"  1-5, 8-15, 25
2686	39	6	4	-25.28	2025-03-27 01:24:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2687	39	7	2	92.29	2024-11-28 19:24:05.85	Генерация: "Reagents"  1-5, 8-15, 25
2688	39	5	5	81.89	2025-04-27 17:49:56.85	Генерация: "Reagents"  1-5, 8-15, 25
2689	39	5	1	47.93	2025-12-14 00:25:30.85	Генерация: "Reagents"  1-5, 8-15, 25
2690	39	8	3	94.71	2024-06-22 08:03:33.85	Генерация: "Reagents"  1-5, 8-15, 25
2691	39	7	5	-21.46	2024-04-11 04:53:16.85	Генерация: "Reagents"  1-5, 8-15, 25
2692	39	7	1	33.15	2025-06-02 14:47:36.85	Генерация: "Reagents"  1-5, 8-15, 25
2693	39	7	2	99.92	2024-09-10 12:19:59.85	Генерация: "Reagents"  1-5, 8-15, 25
2694	39	8	3	-31.08	2026-01-31 12:37:37.85	Генерация: "Reagents"  1-5, 8-15, 25
2695	39	5	3	-19.07	2025-08-09 21:45:53.85	Генерация: "Reagents"  1-5, 8-15, 25
2696	39	5	1	6.44	2024-09-22 05:04:06.85	Генерация: "Reagents"  1-5, 8-15, 25
2697	39	6	1	9.77	2024-10-21 04:46:27.85	Генерация: "Reagents"  1-5, 8-15, 25
2698	39	7	1	40.74	2025-11-25 13:41:16.85	Генерация: "Reagents"  1-5, 8-15, 25
2699	39	6	2	35.91	2025-02-15 09:07:15.85	Генерация: "Reagents"  1-5, 8-15, 25
2700	39	8	2	-34.85	2024-08-16 19:46:43.85	Генерация: "Reagents"  1-5, 8-15, 25
2701	39	6	3	-17.34	2024-09-01 16:41:28.85	Генерация: "Reagents"  1-5, 8-15, 25
2702	39	5	5	20.73	2025-02-10 23:06:37.85	Генерация: "Reagents"  1-5, 8-15, 25
2703	39	5	1	10.02	2025-09-26 07:16:52.85	Генерация: "Reagents"  1-5, 8-15, 25
2704	39	7	3	46.52	2024-12-20 01:24:48.85	Генерация: "Reagents"  1-5, 8-15, 25
2705	39	6	1	20.25	2024-05-29 00:15:26.85	Генерация: "Reagents"  1-5, 8-15, 25
2706	5	7	5	52.32	2026-04-14 00:00:00	Генерация: "Reagents"  1-5, 8-15, 25
2707	5	7	5	-50.00	2026-04-14 00:00:00	Генерация: "Reagents"  1-5, 8-15, 25
2708	4	7	1	10.00	2026-04-14 00:00:00	Генерация: "Reagents"  1-5, 8-15, 25
2709	3	5	4	-5.00	2026-04-14 00:00:00	Генерация: "Reagents"  1-5, 8-15, 25
2710	1	7	3	-25.00	2026-04-14 00:00:00	Генерация: "Reagents"  1-5, 8-15, 25
2715	51	5	2	-1.00	2026-04-14 19:04:40.71	Test
2737	3	5	4	-35.50	2024-09-06 09:48:14.51	Генерация: "Reagents"  1-5, 8-15, 25
2738	51	5	3	9.70	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2739	5	5	3	94.57	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2740	3	5	3	-39.67	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2741	3	6	2	-47.05	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2742	3	7	1	67.18	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2743	3	6	3	95.86	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2744	3	7	3	84.18	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2745	3	8	4	40.53	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2746	30	5	1	40.22	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2747	30	7	3	-36.99	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2748	30	5	2	4.19	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2749	30	6	1	45.99	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2750	30	7	2	71.25	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2751	38	5	5	49.18	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2752	38	7	5	-12.12	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
2753	38	5	3	-1.84	2026-04-15 12:44:08.41	Генерация: "Reagents"  1-5, 8-15, 25
3718	51	7	3	-26.00	2026-06-14 19:15:36	Ежедневная генерация 2026
3719	51	5	1	15.05	2026-06-04 06:38:02	Ежедневная генерация 2026
3720	51	8	4	-8.69	2026-07-30 18:39:02	Ежедневная генерация 2026
3721	51	6	3	-25.24	2026-07-02 16:26:54	Ежедневная генерация 2026
3722	51	8	4	-38.92	2026-06-23 23:40:54	Ежедневная генерация 2026
3723	51	5	2	-48.76	2026-04-23 12:40:33	Ежедневная генерация 2026
3724	51	5	5	-17.29	2026-07-15 19:07:06	Ежедневная генерация 2026
3725	51	7	1	16.97	2026-07-04 06:37:19	Ежедневная генерация 2026
3726	51	6	4	-44.94	2026-05-03 05:51:23	Ежедневная генерация 2026
3727	51	6	1	20.77	2026-06-12 18:04:25	Ежедневная генерация 2026
3728	51	7	5	-20.55	2026-07-29 03:04:22	Ежедневная генерация 2026
3729	51	6	3	-13.09	2026-05-05 07:33:17	Ежедневная генерация 2026
3730	51	8	5	-18.34	2026-08-13 21:19:04	Ежедневная генерация 2026
3731	51	7	1	35.87	2026-05-23 20:45:48	Ежедневная генерация 2026
3732	51	5	5	-8.69	2026-05-05 06:39:41	Ежедневная генерация 2026
3733	51	6	3	-32.40	2026-05-31 03:25:53	Ежедневная генерация 2026
3734	51	7	2	-38.70	2026-05-13 11:59:23	Ежедневная генерация 2026
3735	51	7	5	-37.00	2026-05-28 06:04:31	Ежедневная генерация 2026
3736	51	7	4	-39.01	2026-08-12 19:43:39	Ежедневная генерация 2026
3737	51	7	4	-3.97	2026-07-17 14:06:33	Ежедневная генерация 2026
3738	51	5	3	-23.11	2026-06-22 19:26:17	Ежедневная генерация 2026
3739	51	6	1	26.42	2026-06-01 11:23:30	Ежедневная генерация 2026
3740	51	8	4	-2.91	2026-06-04 02:09:47	Ежедневная генерация 2026
3741	51	7	2	-10.31	2026-05-11 02:41:54	Ежедневная генерация 2026
3742	51	5	2	-25.66	2026-05-01 00:11:43	Ежедневная генерация 2026
3743	51	8	5	-26.48	2026-07-03 20:30:13	Ежедневная генерация 2026
3744	51	5	2	-36.54	2026-06-07 05:48:28	Ежедневная генерация 2026
3745	51	7	1	15.95	2026-05-07 05:13:18	Ежедневная генерация 2026
3746	51	5	5	-36.64	2026-07-12 00:46:54	Ежедневная генерация 2026
3747	51	7	4	-10.83	2026-07-06 04:20:10	Ежедневная генерация 2026
3748	51	7	3	-21.60	2026-07-16 04:16:09	Ежедневная генерация 2026
3749	51	5	5	-18.76	2026-05-13 18:01:17	Ежедневная генерация 2026
3750	51	7	1	15.80	2026-07-05 07:05:41	Ежедневная генерация 2026
3751	51	5	5	-41.26	2026-04-26 18:15:57	Ежедневная генерация 2026
3752	51	6	5	-38.98	2026-05-05 16:42:47	Ежедневная генерация 2026
3753	51	7	1	38.63	2026-08-04 02:50:19	Ежедневная генерация 2026
3754	51	7	2	-41.54	2026-08-12 23:07:05	Ежедневная генерация 2026
3755	51	8	2	-2.85	2026-07-02 03:57:47	Ежедневная генерация 2026
3756	51	8	2	-41.62	2026-05-13 04:14:19	Ежедневная генерация 2026
3757	51	7	1	49.27	2026-07-07 00:36:43	Ежедневная генерация 2026
3758	51	7	5	-25.33	2026-06-15 02:14:47	Ежедневная генерация 2026
3759	51	8	4	-22.39	2026-05-17 00:02:24	Ежедневная генерация 2026
3760	51	8	4	-28.04	2026-07-27 05:00:52	Ежедневная генерация 2026
3761	51	8	1	28.21	2026-05-23 21:10:18	Ежедневная генерация 2026
3762	51	6	4	-28.83	2026-08-02 18:52:15	Ежедневная генерация 2026
3763	51	5	1	39.40	2026-07-20 11:00:50	Ежедневная генерация 2026
3764	51	8	2	-35.41	2026-07-07 21:41:00	Ежедневная генерация 2026
3765	51	6	3	-45.35	2026-07-03 00:22:10	Ежедневная генерация 2026
3766	51	7	4	-26.38	2026-05-21 21:25:55	Ежедневная генерация 2026
3767	51	5	4	-23.11	2026-06-18 16:37:55	Ежедневная генерация 2026
3768	51	8	2	-23.03	2026-08-03 08:47:21	Ежедневная генерация 2026
3769	51	5	3	-41.21	2026-06-01 14:36:33	Ежедневная генерация 2026
3770	51	8	3	-26.50	2026-07-31 17:58:47	Ежедневная генерация 2026
3771	51	8	5	-26.20	2026-05-07 19:35:02	Ежедневная генерация 2026
3772	51	5	5	-44.71	2026-07-06 11:28:40	Ежедневная генерация 2026
3773	51	7	3	-12.69	2026-06-30 17:55:27	Ежедневная генерация 2026
3774	51	8	4	-29.01	2026-05-22 20:40:43	Ежедневная генерация 2026
3775	51	6	3	-42.32	2026-05-31 04:05:08	Ежедневная генерация 2026
3776	51	5	1	19.22	2026-07-29 16:04:34	Ежедневная генерация 2026
3777	51	6	5	-4.59	2026-07-26 11:35:24	Ежедневная генерация 2026
3778	51	7	3	-39.68	2026-08-13 00:40:06	Ежедневная генерация 2026
3779	51	6	1	10.51	2026-06-12 09:52:01	Ежедневная генерация 2026
3780	51	5	4	-30.16	2026-07-25 03:24:42	Ежедневная генерация 2026
3781	51	6	1	9.59	2026-06-30 23:44:22	Ежедневная генерация 2026
3782	51	8	1	11.02	2026-06-12 16:34:11	Ежедневная генерация 2026
3783	51	6	5	-39.70	2026-05-01 17:48:56	Ежедневная генерация 2026
3784	51	5	1	31.65	2026-08-13 21:41:24	Ежедневная генерация 2026
3785	51	7	5	-42.19	2026-06-22 20:49:34	Ежедневная генерация 2026
3786	51	8	5	-26.42	2026-05-10 00:51:24	Ежедневная генерация 2026
3787	51	6	1	26.81	2026-08-15 07:55:53	Ежедневная генерация 2026
3788	51	5	1	15.99	2026-06-08 10:12:11	Ежедневная генерация 2026
3789	51	7	2	-5.57	2026-06-28 03:38:48	Ежедневная генерация 2026
3790	51	8	2	-20.70	2026-07-19 05:25:09	Ежедневная генерация 2026
3791	51	7	1	4.92	2026-06-28 19:25:32	Ежедневная генерация 2026
3792	51	5	1	20.11	2026-06-27 19:25:17	Ежедневная генерация 2026
3793	51	8	3	-3.07	2026-06-27 21:53:30	Ежедневная генерация 2026
3794	51	7	4	-12.71	2026-08-03 11:08:32	Ежедневная генерация 2026
3795	51	7	2	-1.46	2026-05-15 10:44:21	Ежедневная генерация 2026
3796	51	5	5	-46.30	2026-06-12 15:16:37	Ежедневная генерация 2026
3797	51	6	4	-36.91	2026-06-03 07:18:04	Ежедневная генерация 2026
3798	51	5	4	-11.52	2026-07-19 22:07:39	Ежедневная генерация 2026
3799	51	6	1	16.12	2026-06-10 03:09:08	Ежедневная генерация 2026
3800	51	8	3	-36.65	2026-06-30 10:45:04	Ежедневная генерация 2026
3801	51	7	1	8.48	2026-07-13 10:57:14	Ежедневная генерация 2026
3802	51	6	5	-25.59	2026-07-03 06:13:03	Ежедневная генерация 2026
3803	51	6	1	40.54	2026-05-01 21:21:53	Ежедневная генерация 2026
3804	51	5	5	-19.14	2026-08-02 20:41:15	Ежедневная генерация 2026
3805	51	5	4	-45.62	2026-04-22 03:56:29	Ежедневная генерация 2026
3806	51	6	5	-19.70	2026-05-10 14:57:36	Ежедневная генерация 2026
3807	51	8	3	-23.94	2026-05-28 12:05:55	Ежедневная генерация 2026
3808	51	5	5	-48.60	2026-06-29 21:49:21	Ежедневная генерация 2026
3809	51	5	5	-10.19	2026-04-26 18:24:53	Ежедневная генерация 2026
3810	51	8	1	46.47	2026-05-26 20:08:20	Ежедневная генерация 2026
3811	51	7	1	29.52	2026-07-30 22:07:27	Ежедневная генерация 2026
3812	51	5	3	-5.21	2026-06-30 14:36:22	Ежедневная генерация 2026
3813	51	8	3	-2.70	2026-08-01 21:12:08	Ежедневная генерация 2026
3814	51	8	4	-23.70	2026-06-13 12:35:57	Ежедневная генерация 2026
3815	51	8	2	-24.38	2026-07-28 16:21:45	Ежедневная генерация 2026
3816	51	8	2	-3.56	2026-04-26 17:05:51	Ежедневная генерация 2026
3817	51	8	4	-30.42	2026-07-23 09:19:50	Ежедневная генерация 2026
3818	51	7	1	41.72	2026-05-14 11:43:19	Ежедневная генерация 2026
3819	51	8	4	-41.23	2026-04-23 07:31:52	Ежедневная генерация 2026
3820	51	6	3	-49.63	2026-06-22 23:06:27	Ежедневная генерация 2026
3821	51	5	4	-6.65	2026-05-10 15:37:21	Ежедневная генерация 2026
3822	51	6	1	31.25	2026-07-24 04:05:15	Ежедневная генерация 2026
3823	51	5	2	-46.62	2026-04-21 05:47:54	Ежедневная генерация 2026
3824	51	5	1	29.03	2026-05-28 11:40:11	Ежедневная генерация 2026
3825	51	8	5	-33.51	2026-08-04 19:08:21	Ежедневная генерация 2026
3826	51	8	5	-26.41	2026-05-20 06:34:12	Ежедневная генерация 2026
3827	51	6	2	-32.11	2026-06-30 14:28:26	Ежедневная генерация 2026
3828	51	6	1	19.65	2026-06-06 09:37:41	Ежедневная генерация 2026
3829	51	7	5	-31.77	2026-05-09 12:20:33	Ежедневная генерация 2026
3830	51	7	1	23.24	2026-08-06 11:56:07	Ежедневная генерация 2026
3831	51	6	1	21.02	2026-04-20 01:08:04	Ежедневная генерация 2026
3832	51	6	5	-32.76	2026-04-18 02:56:25	Ежедневная генерация 2026
3833	51	8	2	-1.53	2026-06-14 06:30:51	Ежедневная генерация 2026
3834	51	7	4	-49.84	2026-05-04 23:36:11	Ежедневная генерация 2026
3835	51	7	5	-13.02	2026-05-10 10:15:06	Ежедневная генерация 2026
3836	51	8	5	-24.26	2026-07-14 12:35:25	Ежедневная генерация 2026
3837	51	8	3	-24.66	2026-06-08 13:22:56	Ежедневная генерация 2026
3838	51	6	3	-40.74	2026-06-22 07:42:13	Ежедневная генерация 2026
3839	51	7	4	-38.67	2026-04-16 06:46:04	Ежедневная генерация 2026
3840	51	5	4	-31.51	2026-06-26 12:06:08	Ежедневная генерация 2026
3841	51	7	5	-46.03	2026-08-16 11:46:52	Ежедневная генерация 2026
3842	51	6	1	5.19	2026-05-21 12:19:34	Ежедневная генерация 2026
3843	51	7	2	-1.42	2026-05-08 06:39:06	Ежедневная генерация 2026
3844	51	7	1	49.98	2026-07-12 21:45:58	Ежедневная генерация 2026
3845	51	6	3	-31.63	2026-07-06 14:03:20	Ежедневная генерация 2026
3846	51	8	1	28.23	2026-06-22 17:10:14	Ежедневная генерация 2026
3847	51	8	3	-18.36	2026-04-16 16:08:30	Ежедневная генерация 2026
3848	51	5	3	-31.68	2026-05-04 14:35:48	Ежедневная генерация 2026
3849	51	8	3	-1.24	2026-05-27 14:08:58	Ежедневная генерация 2026
3850	51	7	3	-38.68	2026-08-16 19:57:37	Ежедневная генерация 2026
3851	51	5	5	-30.08	2026-07-26 06:23:38	Ежедневная генерация 2026
3852	51	7	4	-35.25	2026-07-04 00:46:38	Ежедневная генерация 2026
3853	51	7	3	-46.12	2026-05-05 21:10:18	Ежедневная генерация 2026
3854	51	6	2	-14.17	2026-06-09 07:40:29	Ежедневная генерация 2026
3855	51	8	1	38.38	2026-07-31 16:34:13	Ежедневная генерация 2026
3856	51	6	2	-37.57	2026-04-27 12:14:45	Ежедневная генерация 2026
3857	51	7	5	-36.80	2026-06-29 06:33:28	Ежедневная генерация 2026
3858	51	6	3	-36.45	2026-04-25 14:21:37	Ежедневная генерация 2026
3859	51	5	4	-3.92	2026-07-16 10:16:58	Ежедневная генерация 2026
3860	51	5	3	-29.05	2026-07-13 08:13:53	Ежедневная генерация 2026
3861	51	8	4	-18.93	2026-05-26 03:08:23	Ежедневная генерация 2026
3862	51	8	1	43.57	2026-06-05 09:47:54	Ежедневная генерация 2026
3863	51	6	1	44.16	2026-08-08 23:06:01	Ежедневная генерация 2026
3864	51	7	5	-8.43	2026-06-03 14:10:10	Ежедневная генерация 2026
3865	51	7	2	-2.56	2026-05-10 07:37:23	Ежедневная генерация 2026
3866	51	7	1	35.83	2026-04-26 01:03:50	Ежедневная генерация 2026
3867	51	5	1	46.15	2026-06-30 16:47:16	Ежедневная генерация 2026
3868	51	6	4	-10.82	2026-08-16 11:42:12	Ежедневная генерация 2026
3869	51	8	3	-35.43	2026-04-30 09:19:57	Ежедневная генерация 2026
3870	51	7	1	21.25	2026-08-02 08:37:12	Ежедневная генерация 2026
3871	51	7	2	-5.25	2026-05-06 19:44:28	Ежедневная генерация 2026
3872	51	7	1	26.86	2026-06-03 16:58:06	Ежедневная генерация 2026
3873	51	8	5	-24.76	2026-06-11 17:12:44	Ежедневная генерация 2026
3874	51	7	3	-42.61	2026-05-15 19:05:22	Ежедневная генерация 2026
3875	51	6	4	-22.36	2026-08-04 00:34:18	Ежедневная генерация 2026
3876	51	8	4	-4.49	2026-05-05 03:56:20	Ежедневная генерация 2026
3877	51	7	1	17.78	2026-07-16 22:02:28	Ежедневная генерация 2026
3878	51	6	5	-29.17	2026-05-15 15:12:10	Ежедневная генерация 2026
3879	51	8	3	-24.70	2026-05-30 02:47:16	Ежедневная генерация 2026
3880	51	8	5	-47.39	2026-06-12 04:00:15	Ежедневная генерация 2026
3881	51	7	1	18.65	2026-07-04 07:05:40	Ежедневная генерация 2026
3882	51	5	4	-18.89	2026-05-02 09:16:33	Ежедневная генерация 2026
3883	51	5	3	-33.56	2026-07-28 21:10:57	Ежедневная генерация 2026
3884	51	8	3	-27.18	2026-06-14 11:38:15	Ежедневная генерация 2026
3885	51	6	1	49.03	2026-05-01 19:55:05	Ежедневная генерация 2026
3886	51	8	1	5.35	2026-05-05 11:38:29	Ежедневная генерация 2026
3887	51	8	4	-3.20	2026-07-05 23:02:33	Ежедневная генерация 2026
3888	51	8	4	-7.20	2026-05-15 14:41:57	Ежедневная генерация 2026
3889	51	8	1	41.25	2026-07-30 18:23:39	Ежедневная генерация 2026
3890	51	5	2	-37.56	2026-07-13 02:17:28	Ежедневная генерация 2026
3891	51	5	3	-31.04	2026-05-23 18:19:11	Ежедневная генерация 2026
3892	51	5	2	-19.85	2026-07-02 23:03:56	Ежедневная генерация 2026
3893	51	7	4	-31.90	2026-07-21 14:39:28	Ежедневная генерация 2026
3894	51	8	4	-17.93	2026-05-30 17:33:51	Ежедневная генерация 2026
3895	51	8	2	-8.19	2026-07-12 17:21:40	Ежедневная генерация 2026
3896	51	7	2	-4.44	2026-07-16 21:02:42	Ежедневная генерация 2026
3897	51	5	4	-20.44	2026-06-17 19:32:13	Ежедневная генерация 2026
3898	51	6	3	-26.40	2026-05-20 15:42:58	Ежедневная генерация 2026
3899	51	5	3	-8.38	2026-07-29 00:38:26	Ежедневная генерация 2026
3900	51	7	4	-40.33	2026-06-03 22:42:58	Ежедневная генерация 2026
3901	51	5	4	-4.92	2026-06-07 10:28:06	Ежедневная генерация 2026
3902	51	8	5	-36.87	2026-08-13 02:33:30	Ежедневная генерация 2026
3903	51	8	4	-2.95	2026-07-03 13:45:41	Ежедневная генерация 2026
3904	51	7	5	-43.06	2026-05-02 09:02:00	Ежедневная генерация 2026
3905	51	6	2	-4.90	2026-06-30 02:15:06	Ежедневная генерация 2026
3906	51	8	3	-21.00	2026-07-03 12:23:35	Ежедневная генерация 2026
3907	51	5	1	20.89	2026-05-02 19:04:51	Ежедневная генерация 2026
3908	51	7	4	-49.36	2026-06-16 11:41:26	Ежедневная генерация 2026
3909	51	8	4	-9.34	2026-07-13 06:03:49	Ежедневная генерация 2026
3910	51	5	1	36.06	2026-06-29 09:47:15	Ежедневная генерация 2026
3911	51	5	2	-25.00	2026-06-24 08:15:31	Ежедневная генерация 2026
3912	51	5	5	-1.40	2026-07-07 00:40:17	Ежедневная генерация 2026
3913	51	8	1	29.08	2026-04-25 03:13:57	Ежедневная генерация 2026
3914	51	6	4	-1.04	2026-05-09 23:50:20	Ежедневная генерация 2026
3915	51	7	4	-23.89	2026-04-17 18:04:52	Ежедневная генерация 2026
3916	51	7	4	-34.71	2026-04-27 06:09:58	Ежедневная генерация 2026
3917	51	5	4	-42.96	2026-07-28 20:09:06	Ежедневная генерация 2026
3918	51	6	1	2.40	2026-05-06 00:56:19	Ежедневная генерация 2026
3919	11	8	2	-33.39	2026-06-10 22:20:16	Ежедневная генерация 2026
3920	11	7	2	-22.04	2026-05-10 06:30:10	Ежедневная генерация 2026
3921	11	5	1	48.24	2026-07-15 09:02:04	Ежедневная генерация 2026
3922	11	6	4	-19.45	2026-07-27 06:48:01	Ежедневная генерация 2026
3923	11	6	5	-26.87	2026-05-30 17:29:59	Ежедневная генерация 2026
3924	11	6	2	-6.59	2026-07-19 20:15:45	Ежедневная генерация 2026
3925	11	5	3	-3.08	2026-04-25 18:26:02	Ежедневная генерация 2026
3926	11	6	1	12.57	2026-07-19 20:29:45	Ежедневная генерация 2026
3927	11	5	1	20.28	2026-05-08 22:46:59	Ежедневная генерация 2026
3928	11	5	2	-25.95	2026-07-13 09:50:54	Ежедневная генерация 2026
3929	11	8	4	-45.24	2026-07-25 17:52:48	Ежедневная генерация 2026
3930	11	6	2	-38.14	2026-06-25 05:34:53	Ежедневная генерация 2026
3931	11	7	1	6.32	2026-06-15 06:52:36	Ежедневная генерация 2026
3932	11	7	1	8.25	2026-05-31 17:50:12	Ежедневная генерация 2026
3933	11	5	2	-2.96	2026-06-28 18:53:10	Ежедневная генерация 2026
3934	11	5	1	39.47	2026-07-15 04:12:31	Ежедневная генерация 2026
3935	11	5	1	16.10	2026-06-06 21:38:32	Ежедневная генерация 2026
3936	11	8	5	-14.84	2026-05-12 11:13:50	Ежедневная генерация 2026
3937	11	6	4	-26.60	2026-04-24 04:18:09	Ежедневная генерация 2026
3938	11	8	3	-40.17	2026-07-09 08:58:18	Ежедневная генерация 2026
3939	11	5	5	-37.23	2026-05-28 23:51:50	Ежедневная генерация 2026
3940	11	7	2	-25.03	2026-05-24 12:33:59	Ежедневная генерация 2026
3941	11	5	2	-29.20	2026-04-16 10:09:36	Ежедневная генерация 2026
3942	11	7	5	-9.43	2026-06-16 01:13:10	Ежедневная генерация 2026
3943	11	5	1	6.34	2026-08-04 06:51:58	Ежедневная генерация 2026
3944	11	8	4	-32.58	2026-05-03 09:22:56	Ежедневная генерация 2026
3945	11	6	1	16.99	2026-05-11 15:49:13	Ежедневная генерация 2026
3946	11	8	3	-11.01	2026-07-26 17:22:40	Ежедневная генерация 2026
3947	11	5	4	-47.12	2026-05-25 16:32:44	Ежедневная генерация 2026
3948	11	6	2	-49.48	2026-04-21 02:07:25	Ежедневная генерация 2026
3949	11	6	2	-4.83	2026-05-04 05:33:06	Ежедневная генерация 2026
3950	11	8	2	-15.95	2026-07-12 22:07:50	Ежедневная генерация 2026
3951	11	6	3	-46.70	2026-05-11 22:37:06	Ежедневная генерация 2026
3952	11	7	4	-12.35	2026-05-16 19:30:44	Ежедневная генерация 2026
3953	11	8	5	-11.39	2026-05-08 13:03:57	Ежедневная генерация 2026
3954	11	5	3	-32.83	2026-07-31 03:51:33	Ежедневная генерация 2026
3955	11	6	4	-46.60	2026-07-25 08:25:41	Ежедневная генерация 2026
3956	11	6	1	44.30	2026-06-18 20:21:39	Ежедневная генерация 2026
3957	11	6	5	-36.43	2026-05-07 14:35:51	Ежедневная генерация 2026
3958	11	8	5	-10.48	2026-07-01 16:10:28	Ежедневная генерация 2026
3959	11	6	2	-13.06	2026-06-07 15:10:15	Ежедневная генерация 2026
3960	11	5	3	-45.76	2026-06-16 06:49:23	Ежедневная генерация 2026
3961	11	6	5	-42.12	2026-06-29 09:29:04	Ежедневная генерация 2026
3962	11	8	2	-39.84	2026-06-18 19:29:14	Ежедневная генерация 2026
3963	11	8	1	45.20	2026-05-19 08:00:32	Ежедневная генерация 2026
3964	11	5	3	-28.14	2026-06-11 21:47:02	Ежедневная генерация 2026
3965	11	6	3	-4.47	2026-05-24 21:45:22	Ежедневная генерация 2026
3966	11	7	3	-40.68	2026-06-11 08:37:13	Ежедневная генерация 2026
3967	11	7	4	-3.00	2026-05-02 13:42:22	Ежедневная генерация 2026
3968	11	8	4	-30.76	2026-05-30 14:26:07	Ежедневная генерация 2026
3969	11	8	1	1.22	2026-07-19 10:23:59	Ежедневная генерация 2026
3970	11	6	3	-25.89	2026-06-28 07:15:39	Ежедневная генерация 2026
3971	11	5	3	-26.94	2026-07-18 10:08:52	Ежедневная генерация 2026
3972	11	5	2	-9.95	2026-06-12 23:41:17	Ежедневная генерация 2026
3973	11	7	1	17.66	2026-05-19 00:30:51	Ежедневная генерация 2026
3974	11	8	3	-30.49	2026-04-25 19:08:46	Ежедневная генерация 2026
3975	11	8	2	-13.15	2026-06-09 01:45:05	Ежедневная генерация 2026
3976	11	7	3	-40.61	2026-07-21 07:28:30	Ежедневная генерация 2026
3977	11	6	5	-6.01	2026-07-15 15:26:04	Ежедневная генерация 2026
3978	11	6	5	-30.05	2026-07-14 19:30:23	Ежедневная генерация 2026
3979	11	7	2	-27.13	2026-05-02 22:01:51	Ежедневная генерация 2026
3980	11	8	4	-48.90	2026-07-04 03:16:51	Ежедневная генерация 2026
3981	11	5	2	-31.89	2026-08-10 18:26:11	Ежедневная генерация 2026
3982	11	8	5	-1.52	2026-07-14 09:41:28	Ежедневная генерация 2026
3983	11	5	3	-8.55	2026-05-04 00:36:06	Ежедневная генерация 2026
3984	11	5	1	10.63	2026-05-01 07:55:51	Ежедневная генерация 2026
3985	11	5	3	-36.95	2026-06-17 19:41:16	Ежедневная генерация 2026
3986	11	5	1	18.76	2026-07-11 22:50:53	Ежедневная генерация 2026
3987	11	5	5	-34.51	2026-04-21 13:07:10	Ежедневная генерация 2026
3988	11	5	5	-35.34	2026-07-18 19:11:29	Ежедневная генерация 2026
3989	11	5	2	-29.41	2026-08-08 19:59:44	Ежедневная генерация 2026
3990	11	6	2	-42.21	2026-05-06 14:51:14	Ежедневная генерация 2026
3991	11	5	5	-25.66	2026-04-27 04:14:33	Ежедневная генерация 2026
3992	11	6	5	-10.74	2026-07-04 09:53:39	Ежедневная генерация 2026
3993	11	8	3	-9.30	2026-07-02 03:18:53	Ежедневная генерация 2026
3994	11	6	3	-2.31	2026-07-09 10:12:41	Ежедневная генерация 2026
3995	11	8	1	41.08	2026-05-21 15:51:42	Ежедневная генерация 2026
3996	11	6	5	-29.41	2026-06-24 22:29:30	Ежедневная генерация 2026
3997	11	6	5	-15.27	2026-05-25 21:32:35	Ежедневная генерация 2026
3998	11	7	3	-31.62	2026-05-19 22:02:43	Ежедневная генерация 2026
3999	11	6	5	-10.22	2026-06-10 12:08:38	Ежедневная генерация 2026
4000	11	6	4	-21.55	2026-06-05 11:42:54	Ежедневная генерация 2026
4001	11	6	3	-29.20	2026-05-26 02:33:50	Ежедневная генерация 2026
4002	11	8	3	-40.68	2026-07-24 14:42:57	Ежедневная генерация 2026
4003	11	5	5	-41.85	2026-07-04 13:32:11	Ежедневная генерация 2026
4004	11	8	5	-35.70	2026-05-11 07:28:50	Ежедневная генерация 2026
4005	11	5	4	-33.52	2026-06-30 22:05:40	Ежедневная генерация 2026
4006	11	7	5	-22.95	2026-07-13 17:17:28	Ежедневная генерация 2026
4007	11	8	5	-16.11	2026-04-23 07:15:54	Ежедневная генерация 2026
4008	11	8	2	-21.73	2026-08-14 06:03:31	Ежедневная генерация 2026
4009	11	8	5	-49.17	2026-06-24 17:08:17	Ежедневная генерация 2026
4010	11	6	2	-46.01	2026-08-07 01:21:26	Ежедневная генерация 2026
4011	11	7	2	-37.79	2026-04-16 13:57:47	Ежедневная генерация 2026
4012	51	7	3	-30.26	2026-06-30 09:45:54	Ежедневная генерация 2026
4013	51	5	2	-8.45	2026-05-28 16:30:59	Ежедневная генерация 2026
4014	25	7	4	-12.13	2026-06-09 05:02:46	Ежедневная генерация 2026
4015	25	8	1	40.97	2026-06-21 03:31:59	Ежедневная генерация 2026
4016	25	5	3	-15.80	2026-06-01 21:13:34	Ежедневная генерация 2026
4017	25	6	2	-15.50	2026-07-24 20:54:47	Ежедневная генерация 2026
4018	25	6	1	31.04	2026-07-29 10:54:39	Ежедневная генерация 2026
4019	25	6	5	-25.71	2026-08-05 23:49:54	Ежедневная генерация 2026
4020	25	6	3	-15.52	2026-04-20 04:04:56	Ежедневная генерация 2026
4021	25	6	4	-23.60	2026-07-12 09:29:20	Ежедневная генерация 2026
4022	25	7	5	-2.22	2026-05-08 16:17:43	Ежедневная генерация 2026
4023	25	7	1	26.43	2026-06-29 02:02:50	Ежедневная генерация 2026
4024	25	7	2	-39.41	2026-07-11 03:11:11	Ежедневная генерация 2026
4025	25	6	5	-34.41	2026-07-20 00:18:35	Ежедневная генерация 2026
4026	25	7	3	-11.60	2026-05-03 10:02:32	Ежедневная генерация 2026
4027	25	6	3	-32.21	2026-06-14 20:13:35	Ежедневная генерация 2026
4028	25	8	2	-11.89	2026-08-01 20:20:21	Ежедневная генерация 2026
4029	25	8	3	-32.77	2026-06-25 22:06:59	Ежедневная генерация 2026
4030	25	8	1	3.51	2026-08-04 01:46:06	Ежедневная генерация 2026
4031	25	7	1	22.61	2026-04-18 04:06:52	Ежедневная генерация 2026
4032	25	8	2	-12.27	2026-08-02 01:49:20	Ежедневная генерация 2026
4033	25	7	1	37.37	2026-05-08 07:44:13	Ежедневная генерация 2026
4034	25	6	1	45.65	2026-08-01 01:35:15	Ежедневная генерация 2026
4035	25	6	1	12.55	2026-08-04 01:24:42	Ежедневная генерация 2026
4036	32	5	5	-7.20	2026-07-31 13:52:13	Ежедневная генерация 2026
4037	32	6	1	16.98	2026-07-13 03:04:27	Ежедневная генерация 2026
4038	32	8	1	2.62	2026-08-08 15:14:52	Ежедневная генерация 2026
4039	32	7	1	18.82	2026-05-29 01:44:34	Ежедневная генерация 2026
4040	32	8	5	-48.10	2026-04-30 14:23:27	Ежедневная генерация 2026
4041	32	6	4	-39.98	2026-06-02 18:35:43	Ежедневная генерация 2026
4042	32	6	1	15.35	2026-05-12 23:41:04	Ежедневная генерация 2026
4043	32	7	4	-20.88	2026-06-11 09:45:57	Ежедневная генерация 2026
4044	32	7	3	-22.22	2026-06-09 12:47:53	Ежедневная генерация 2026
4045	32	7	5	-23.84	2026-07-18 20:36:05	Ежедневная генерация 2026
4046	32	8	3	-21.84	2026-08-12 05:00:48	Ежедневная генерация 2026
4047	32	8	5	-4.43	2026-04-25 19:44:21	Ежедневная генерация 2026
4048	32	6	4	-37.56	2026-08-10 19:48:56	Ежедневная генерация 2026
4049	32	5	5	-21.54	2026-04-20 06:42:05	Ежедневная генерация 2026
4050	32	6	5	-21.21	2026-04-27 07:52:43	Ежедневная генерация 2026
4051	32	6	2	-12.59	2026-06-14 17:07:02	Ежедневная генерация 2026
4052	32	6	5	-10.24	2026-07-06 23:41:15	Ежедневная генерация 2026
4053	32	7	4	-49.77	2026-04-29 21:40:27	Ежедневная генерация 2026
4054	32	6	4	-21.46	2026-06-26 08:45:04	Ежедневная генерация 2026
4055	32	6	2	-44.92	2026-07-10 16:42:39	Ежедневная генерация 2026
4056	32	6	5	-30.90	2026-07-01 21:26:15	Ежедневная генерация 2026
4057	32	7	5	-12.05	2026-07-31 07:29:34	Ежедневная генерация 2026
4058	32	7	1	18.06	2026-08-10 04:50:19	Ежедневная генерация 2026
4059	32	5	1	39.26	2026-04-27 09:32:39	Ежедневная генерация 2026
4060	32	8	4	-46.63	2026-06-30 12:07:26	Ежедневная генерация 2026
4061	32	8	1	49.67	2026-08-13 16:29:20	Ежедневная генерация 2026
4062	32	5	2	-48.31	2026-07-22 06:32:39	Ежедневная генерация 2026
4063	32	5	2	-11.01	2026-04-21 21:40:54	Ежедневная генерация 2026
4064	51	7	5	-36.87	2026-07-23 12:54:11	Ежедневная генерация 2026
4065	10	7	4	-11.39	2026-05-17 01:18:23	Ежедневная генерация 2026
4066	10	5	5	-27.80	2026-08-05 06:54:59	Ежедневная генерация 2026
4067	10	8	2	-36.94	2026-07-13 20:57:15	Ежедневная генерация 2026
4068	10	7	4	-10.47	2026-07-25 02:50:08	Ежедневная генерация 2026
4069	10	5	3	-4.86	2026-05-01 07:37:04	Ежедневная генерация 2026
4070	10	5	1	40.88	2026-06-12 15:56:48	Ежедневная генерация 2026
4071	10	5	1	47.93	2026-07-25 01:53:30	Ежедневная генерация 2026
4072	10	7	1	16.62	2026-07-07 07:00:13	Ежедневная генерация 2026
4073	10	7	5	-41.06	2026-05-02 07:09:47	Ежедневная генерация 2026
4074	10	6	1	24.20	2026-08-13 09:48:11	Ежедневная генерация 2026
4075	10	8	3	-36.85	2026-06-26 02:15:57	Ежедневная генерация 2026
4076	10	6	2	-12.67	2026-07-12 17:38:10	Ежедневная генерация 2026
4077	10	8	1	15.73	2026-08-10 05:41:48	Ежедневная генерация 2026
4078	10	5	1	41.58	2026-04-21 14:31:01	Ежедневная генерация 2026
4079	10	8	2	-38.23	2026-07-05 10:57:38	Ежедневная генерация 2026
4080	10	5	1	9.08	2026-06-04 22:49:41	Ежедневная генерация 2026
4081	10	8	2	-21.97	2026-04-24 17:39:28	Ежедневная генерация 2026
4082	10	5	4	-10.21	2026-05-16 05:03:48	Ежедневная генерация 2026
4083	10	7	5	-29.32	2026-07-17 12:58:03	Ежедневная генерация 2026
4084	10	6	1	16.45	2026-07-21 19:06:52	Ежедневная генерация 2026
4085	10	8	4	-14.06	2026-08-07 09:16:15	Ежедневная генерация 2026
4086	10	8	5	-46.11	2026-08-11 03:40:49	Ежедневная генерация 2026
4087	10	8	3	-43.51	2026-06-13 09:06:12	Ежедневная генерация 2026
4088	10	7	1	49.03	2026-07-12 15:06:03	Ежедневная генерация 2026
4089	10	5	3	-26.19	2026-04-19 03:49:29	Ежедневная генерация 2026
4090	10	5	3	-20.68	2026-08-03 13:47:33	Ежедневная генерация 2026
4091	10	8	1	13.67	2026-06-17 03:27:00	Ежедневная генерация 2026
4092	10	7	1	47.62	2026-05-17 23:42:02	Ежедневная генерация 2026
4093	10	5	5	-25.99	2026-05-10 02:50:19	Ежедневная генерация 2026
4094	10	8	1	19.36	2026-07-22 01:43:36	Ежедневная генерация 2026
4095	10	6	3	-22.12	2026-05-10 09:31:18	Ежедневная генерация 2026
4096	10	5	2	-44.28	2026-05-01 07:29:48	Ежедневная генерация 2026
4097	10	7	4	-17.96	2026-06-30 13:24:54	Ежедневная генерация 2026
4098	10	7	4	-23.17	2026-07-09 19:26:16	Ежедневная генерация 2026
4099	10	7	3	-14.29	2026-06-17 09:30:25	Ежедневная генерация 2026
4100	10	8	3	-40.32	2026-05-04 12:40:23	Ежедневная генерация 2026
4101	10	6	2	-41.78	2026-08-02 10:28:58	Ежедневная генерация 2026
4102	51	5	2	-45.00	2026-04-22 14:25:01	Ежедневная генерация 2026
4103	30	7	4	-3.23	2026-05-04 07:20:02	Ежедневная генерация 2026
4104	30	6	1	40.94	2026-08-08 06:33:39	Ежедневная генерация 2026
4105	30	7	3	-8.16	2026-06-29 08:08:08	Ежедневная генерация 2026
4106	30	6	5	-27.31	2026-08-07 15:25:10	Ежедневная генерация 2026
4107	30	7	2	-1.98	2026-06-17 16:43:17	Ежедневная генерация 2026
4108	30	6	4	-19.04	2026-06-02 20:25:56	Ежедневная генерация 2026
4109	30	6	5	-16.16	2026-06-19 15:43:53	Ежедневная генерация 2026
4110	30	5	2	-21.04	2026-04-22 08:22:39	Ежедневная генерация 2026
4111	30	6	3	-42.89	2026-07-11 14:08:34	Ежедневная генерация 2026
4112	30	8	3	-8.08	2026-04-19 10:14:58	Ежедневная генерация 2026
4113	30	5	2	-32.25	2026-05-07 00:36:08	Ежедневная генерация 2026
4114	30	5	1	2.72	2026-06-10 00:29:04	Ежедневная генерация 2026
4115	30	6	3	-17.51	2026-08-09 10:19:23	Ежедневная генерация 2026
4116	30	6	3	-31.76	2026-06-21 04:05:25	Ежедневная генерация 2026
4117	30	7	1	20.86	2026-07-10 04:21:13	Ежедневная генерация 2026
4118	30	5	2	-25.07	2026-05-25 02:50:27	Ежедневная генерация 2026
4119	30	7	5	-15.68	2026-08-13 04:28:15	Ежедневная генерация 2026
4120	30	8	3	-46.42	2026-05-09 06:50:50	Ежедневная генерация 2026
4121	30	8	5	-34.04	2026-06-05 01:51:11	Ежедневная генерация 2026
4122	30	7	4	-18.04	2026-05-10 18:21:21	Ежедневная генерация 2026
4123	30	6	1	44.71	2026-06-05 22:06:20	Ежедневная генерация 2026
4124	30	6	2	-43.08	2026-04-29 06:46:57	Ежедневная генерация 2026
4125	30	6	1	36.38	2026-04-19 05:16:57	Ежедневная генерация 2026
4126	51	5	2	-10.15	2026-06-07 03:50:59	Ежедневная генерация 2026
4127	41	8	3	-32.18	2026-05-20 03:49:16	Ежедневная генерация 2026
4128	41	8	2	-33.06	2026-05-01 03:23:23	Ежедневная генерация 2026
4129	41	6	5	-11.70	2026-07-25 05:53:14	Ежедневная генерация 2026
4130	41	8	2	-14.65	2026-07-09 23:30:55	Ежедневная генерация 2026
4131	41	7	3	-36.29	2026-05-05 20:09:36	Ежедневная генерация 2026
4132	41	5	1	30.00	2026-06-01 22:51:06	Ежедневная генерация 2026
4133	41	8	2	-49.55	2026-08-16 23:53:27	Ежедневная генерация 2026
4134	41	8	3	-7.28	2026-05-19 07:03:18	Ежедневная генерация 2026
4135	41	8	1	12.50	2026-08-07 00:26:30	Ежедневная генерация 2026
4136	41	6	5	-35.12	2026-06-18 05:24:52	Ежедневная генерация 2026
4137	41	6	3	-5.69	2026-08-14 12:34:20	Ежедневная генерация 2026
4138	41	5	5	-5.63	2026-05-14 20:16:30	Ежедневная генерация 2026
4139	41	7	3	-23.29	2026-07-26 12:48:04	Ежедневная генерация 2026
4140	41	8	5	-9.51	2026-04-19 00:31:23	Ежедневная генерация 2026
4141	41	7	2	-7.20	2026-07-25 13:03:17	Ежедневная генерация 2026
4142	41	7	4	-9.02	2026-06-08 02:01:38	Ежедневная генерация 2026
4143	41	7	5	-18.49	2026-05-18 09:08:02	Ежедневная генерация 2026
4144	41	6	1	23.15	2026-05-23 08:31:31	Ежедневная генерация 2026
4145	41	8	2	-14.18	2026-08-15 23:42:38	Ежедневная генерация 2026
4146	41	8	1	42.93	2026-05-26 16:06:08	Ежедневная генерация 2026
4147	41	7	3	-43.09	2026-08-13 21:06:32	Ежедневная генерация 2026
4148	41	6	5	-20.25	2026-05-27 00:15:47	Ежедневная генерация 2026
4149	41	7	5	-23.50	2026-06-26 11:04:03	Ежедневная генерация 2026
4150	51	8	5	-12.43	2026-07-02 22:41:13	Ежедневная генерация 2026
4151	37	6	1	35.05	2026-06-07 21:06:55	Ежедневная генерация 2026
4152	37	5	1	49.91	2026-07-11 03:50:54	Ежедневная генерация 2026
4153	37	8	4	-31.86	2026-07-21 14:44:33	Ежедневная генерация 2026
4154	37	8	2	-40.32	2026-07-29 00:40:18	Ежедневная генерация 2026
4155	37	6	1	41.57	2026-08-09 20:15:18	Ежедневная генерация 2026
4156	37	5	3	-27.30	2026-06-11 22:18:59	Ежедневная генерация 2026
4157	37	5	2	-30.45	2026-05-07 15:58:54	Ежедневная генерация 2026
4158	37	7	3	-14.61	2026-05-27 15:47:52	Ежедневная генерация 2026
4159	37	6	3	-42.18	2026-07-23 22:12:30	Ежедневная генерация 2026
4160	37	6	5	-1.30	2026-04-21 02:10:49	Ежедневная генерация 2026
4161	37	5	5	-48.91	2026-08-15 20:28:56	Ежедневная генерация 2026
4162	37	6	2	-11.70	2026-04-28 09:55:59	Ежедневная генерация 2026
4163	37	5	4	-13.91	2026-04-16 23:59:42	Ежедневная генерация 2026
4164	37	5	4	-22.85	2026-07-20 00:15:26	Ежедневная генерация 2026
4165	37	6	1	20.74	2026-08-04 16:39:26	Ежедневная генерация 2026
4166	37	6	3	-10.36	2026-05-19 07:40:28	Ежедневная генерация 2026
4167	37	6	1	12.72	2026-07-31 16:41:19	Ежедневная генерация 2026
4168	37	8	5	-27.32	2026-05-02 05:43:29	Ежедневная генерация 2026
4169	37	7	5	-25.52	2026-08-13 03:46:36	Ежедневная генерация 2026
4170	37	8	1	27.34	2026-06-27 03:13:17	Ежедневная генерация 2026
4171	37	8	2	-25.03	2026-05-09 17:18:53	Ежедневная генерация 2026
4172	37	6	5	-1.48	2026-06-23 17:00:27	Ежедневная генерация 2026
4173	37	7	3	-10.67	2026-07-28 14:05:05	Ежедневная генерация 2026
4174	37	6	2	-41.21	2026-04-24 14:43:04	Ежедневная генерация 2026
4175	37	6	3	-48.30	2026-05-13 04:06:11	Ежедневная генерация 2026
4176	37	7	4	-34.54	2026-06-01 16:15:30	Ежедневная генерация 2026
4177	37	7	5	-40.82	2026-07-12 01:13:02	Ежедневная генерация 2026
4178	37	7	4	-33.34	2026-08-11 08:16:04	Ежедневная генерация 2026
4179	51	6	2	-4.87	2026-06-02 19:25:48	Ежедневная генерация 2026
4180	3	8	5	-8.23	2026-06-14 05:22:54	Ежедневная генерация 2026
4181	3	8	3	-36.38	2026-06-04 11:31:26	Ежедневная генерация 2026
4182	3	5	1	12.59	2026-07-13 15:34:15	Ежедневная генерация 2026
4183	3	8	4	-37.85	2026-07-17 14:10:45	Ежедневная генерация 2026
4184	3	8	1	14.40	2026-05-12 16:56:45	Ежедневная генерация 2026
4185	3	5	3	-14.96	2026-06-01 05:14:18	Ежедневная генерация 2026
4186	3	8	3	-14.17	2026-05-10 01:42:46	Ежедневная генерация 2026
4187	3	6	1	8.37	2026-05-31 07:37:06	Ежедневная генерация 2026
4188	3	7	1	39.05	2026-06-19 02:45:38	Ежедневная генерация 2026
4189	3	7	1	37.35	2026-05-14 06:56:54	Ежедневная генерация 2026
4190	3	6	3	-10.70	2026-07-01 20:17:59	Ежедневная генерация 2026
4191	3	7	4	-11.11	2026-06-22 19:06:40	Ежедневная генерация 2026
4192	3	6	4	-40.93	2026-04-18 06:25:06	Ежедневная генерация 2026
4193	3	6	2	-24.96	2026-06-29 04:49:13	Ежедневная генерация 2026
4194	3	8	5	-29.03	2026-05-04 00:27:38	Ежедневная генерация 2026
4195	3	6	1	16.42	2026-04-23 14:15:48	Ежедневная генерация 2026
4196	3	5	4	-31.55	2026-05-12 23:08:48	Ежедневная генерация 2026
4197	3	6	1	9.93	2026-08-11 16:54:17	Ежедневная генерация 2026
4198	3	6	4	-48.56	2026-06-05 20:41:36	Ежедневная генерация 2026
4199	51	5	3	-45.58	2026-08-15 01:09:16	Ежедневная генерация 2026
4200	38	6	2	-18.05	2026-05-30 00:20:14	Ежедневная генерация 2026
4201	38	7	5	-15.59	2026-06-11 18:00:07	Ежедневная генерация 2026
4202	38	5	5	-46.54	2026-07-13 21:29:36	Ежедневная генерация 2026
4203	38	6	4	-44.43	2026-05-05 03:08:24	Ежедневная генерация 2026
4204	38	7	5	-36.76	2026-06-12 06:51:26	Ежедневная генерация 2026
4205	38	8	5	-23.00	2026-06-03 07:32:42	Ежедневная генерация 2026
4206	38	5	4	-31.88	2026-05-24 08:23:45	Ежедневная генерация 2026
4207	38	6	5	-43.84	2026-06-15 14:50:33	Ежедневная генерация 2026
4208	38	5	4	-27.49	2026-07-27 09:00:21	Ежедневная генерация 2026
4209	38	7	5	-15.68	2026-06-24 01:05:09	Ежедневная генерация 2026
4210	38	7	4	-45.87	2026-06-05 01:16:41	Ежедневная генерация 2026
4211	38	7	1	37.99	2026-07-26 22:36:56	Ежедневная генерация 2026
4212	38	6	4	-1.57	2026-07-24 23:56:26	Ежедневная генерация 2026
4213	38	7	1	2.53	2026-05-04 13:42:46	Ежедневная генерация 2026
4214	38	6	2	-41.47	2026-06-21 06:43:25	Ежедневная генерация 2026
4215	38	5	5	-1.22	2026-06-27 17:17:58	Ежедневная генерация 2026
4216	38	6	5	-36.84	2026-06-11 06:29:44	Ежедневная генерация 2026
4217	38	6	2	-48.59	2026-04-27 15:41:35	Ежедневная генерация 2026
4226	60	8	1	4.00	2026-04-24 14:19:37.16	Начальное внесение в систему реагента: фыа
4227	60	8	5	4.00	2026-04-24 19:53:59.19	Редактирование параметров реактива
4228	60	8	5	4.00	2026-04-24 19:55:12.5	Редактирование параметров реактива
4229	60	8	5	4.00	2026-04-24 19:55:25.17	Редактирование параметров реактива
4230	61	8	1	89.00	2026-04-24 19:56:10.83	Операция Receipt над реагентом ddd
4231	61	8	3	0.00	2026-04-24 20:10:35.81	Мягкое удаление (архивация)
4232	61	8	3	0.00	2026-04-24 20:12:24.28	Мягкое удаление (архивация)
4233	61	8	3	0.00	2026-04-24 20:18:44.89	Мягкое удаление (архивация)
4234	1	8	3	0.00	2026-04-26 17:50:07.22	Мягкое удаление (архивация)
4235	1	8	3	0.00	2026-04-26 17:50:21.57	Мягкое удаление (архивация)
4236	1	8	3	0.00	2026-04-26 17:54:16.16	Мягкое удаление (архивация)
4237	1	5	3	0.00	2026-04-26 17:54:54.85	Мягкое удаление (архивация)
4238	1	5	3	0.00	2026-04-26 17:56:21.48	Мягкое удаление (архивация)
4239	1	5	3	0.00	2026-04-26 17:57:18	Мягкое удаление (архивация)
4240	1	8	3	0.00	2026-04-26 18:44:12.87	Мягкое удаление (архивация)
4241	1	8	3	0.00	2026-04-26 18:53:49.7	Мягкое удаление (архивация)
4242	1	8	3	0.00	2026-04-26 19:12:18.77	Мягкое удаление (архивация)
4243	1	8	3	0.00	2026-04-26 19:20:43.11	Мягкое удаление (архивация)
4244	1	8	3	0.00	2026-04-26 19:21:07.79	Мягкое удаление (архивация)
4245	1	8	3	0.00	2026-04-26 19:25:52.89	Мягкое удаление (архивация)
4246	1	8	3	0.00	2026-04-26 19:28:46.96	Мягкое удаление (архивация)
4247	1	8	5	0.00	2026-04-26 19:39:08.32	Восстановление данных
4248	1	8	5	0.00	2026-04-26 19:39:45.18	Восстановление данных
4249	1	8	5	0.00	2026-04-26 19:43:28.5	Восстановление данных
4250	1	8	5	0.00	2026-04-26 19:46:51.02	Восстановление данных
4251	1	8	5	0.00	2026-04-26 19:48:55.25	Восстановление данных
4252	1	8	5	0.00	2026-04-26 19:53:34.72	Восстановление данных
4253	62	8	1	15.00	2026-04-28 18:37:11.83	Операция Receipt над реагентом Новый реагент
4254	51	8	3	0.00	2026-04-28 18:40:34.13	Мягкое удаление (архивация)
4255	51	8	5	0.00	2026-04-28 18:41:00.93	Восстановление данных
4256	60	8	3	0.00	2026-05-13 18:10:05.63	Мягкое удаление (архивация)
4257	2	8	3	0.00	2026-05-13 19:05:32.22	Мягкое удаление (архивация)
4258	2	8	5	0.00	2026-05-16 14:51:16.61	Восстановление данных
4259	2	8	3	0.00	2026-05-16 14:51:21.14	Мягкое удаление (архивация)
4260	2	8	5	0.00	2026-05-16 14:51:24.24	Восстановление данных
4261	1	8	5	9.00	2026-05-16 14:52:58.32	Редактирование параметров реактива
4262	1	8	5	9.00	2026-05-16 14:54:40.42	Редактирование параметров реактива
4263	1	8	5	56.00	2026-05-16 14:55:54.95	Редактирование параметров реактива
4264	2	8	5	25.50	2026-05-16 14:56:09.21	Редактирование параметров реактива
4265	1	8	5	56.00	2026-05-16 14:59:14.91	Редактирование параметров реактива
4269	1	8	5	56.00	2026-05-16 15:11:58.72	Редактирование параметров реактива
4270	1	8	5	56.00	2026-05-16 15:12:35.39	Редактирование параметров реактива
4271	1	8	5	5.00	2026-05-16 15:13:36.62	Редактирование параметров реактива
4272	2	8	5	25.50	2026-05-16 15:26:32.76	Редактирование параметров реактива
4273	2	8	5	25.50	2026-05-16 15:27:29.87	Редактирование параметров реактива
4274	1	8	5	5.00	2026-05-16 15:28:29.12	Редактирование параметров реактива
4275	2	8	5	25.50	2026-05-16 15:29:31.23	Редактирование параметров реактива
4276	2	8	5	25.50	2026-05-16 15:30:02.85	Редактирование параметров реактива
4277	2	8	5	25.50	2026-05-16 15:31:02.95	Редактирование параметров реактива
4278	1	8	5	5.00	2026-05-16 15:35:13.88	Редактирование параметров реактива
4279	1	8	5	5.00	2026-05-16 17:21:05.73	Редактирование параметров реактива
4281	1	8	5	5.00	2026-05-16 17:21:15.15	Редактирование параметров реактива
4282	1	8	5	3.00	2026-05-16 18:51:25.84	Редактирование параметров реактива
4283	1	8	5	7.00	2026-05-16 19:01:15.25	Редактирование параметров реактива
4284	1	8	5	2.00	2026-05-17 17:49:06.8	Редактирование параметров реактива
4285	2	8	5	25.50	2026-05-18 10:40:32.85	Массовое редактирование реактивов
4286	3	8	5	15.00	2026-05-18 10:40:32.88	Массовое редактирование реактивов
4287	2	8	5	25.50	2026-05-18 10:45:49.2	Массовое редактирование реактивов
4288	3	8	5	15.00	2026-05-18 10:46:03.07	Массовое редактирование реактивов
4289	3	8	5	15.00	2026-05-18 11:19:40.24	Массовое редактирование реактивов
4290	2	8	5	25.50	2026-05-18 11:19:40.3	Массовое редактирование реактивов
4291	3	8	5	15.00	2026-05-18 11:19:40.24	Массовое редактирование реактивов
4292	2	8	5	25.50	2026-05-18 11:19:40.3	Массовое редактирование реактивов
4293	2	8	5	25.50	2026-05-18 11:39:41.01	Массовое редактирование реактивов
4294	3	8	5	15.00	2026-05-18 11:39:41.01	Массовое редактирование реактивов
4295	2	8	5	25.50	2026-05-18 11:39:41	Массовое редактирование реактивов
4296	3	8	5	15.00	2026-05-18 11:39:41.01	Массовое редактирование реактивов
4297	3	8	5	5.00	2026-05-18 11:40:12.43	Массовое редактирование реактивов
4298	2	8	5	15.50	2026-05-18 11:40:12.42	Массовое редактирование реактивов
4299	4	8	5	30.00	2026-05-18 11:40:37.53	Массовое редактирование реактивов
4300	5	8	5	50.00	2026-05-18 11:40:37.55	Массовое редактирование реактивов
4301	3	8	5	67.00	2026-05-18 11:44:15.68	Массовое редактирование реактивов
4302	4	8	5	92.00	2026-05-18 11:44:15.68	Массовое редактирование реактивов
4303	3	8	5	77.00	2026-05-18 11:47:19.91	Массовое редактирование реактивов
4304	4	8	5	102.00	2026-05-18 11:47:19.92	Массовое редактирование реактивов
4305	3	8	5	87.00	2026-05-18 11:47:28.72	Массовое редактирование реактивов
4306	4	8	5	112.00	2026-05-18 11:47:28.74	Массовое редактирование реактивов
4307	9	8	5	15.50	2026-05-18 12:52:01.37	Массовое редактирование реактивов
4308	9	8	5	25.50	2026-05-18 12:52:08.23	Массовое редактирование реактивов
4309	2	8	5	7.50	2026-05-18 13:49:11.15	Массовое редактирование реактивов
4310	2	8	5	5.50	2026-05-18 14:00:19.26	Массовое редактирование реактивов
4311	9	8	5	5.50	2026-05-18 14:08:47.26	Массовое редактирование реактивов
4312	3	8	5	4.00	2026-05-18 14:56:32.44	Массовое редактирование реактивов
4313	5	8	5	10.00	2026-05-18 14:57:31.36	Массовое редактирование реактивов
4314	1	8	5	22.00	2026-05-18 14:58:46.04	Массовое редактирование реактивов
4315	3	8	5	24.00	2026-05-18 14:58:46.07	Массовое редактирование реактивов
4316	4	8	5	132.00	2026-05-18 14:58:46.09	Массовое редактирование реактивов
4317	2	8	5	25.50	2026-05-18 14:58:46.09	Массовое редактирование реактивов
4318	8	8	5	23.00	2026-05-18 14:58:46.12	Массовое редактирование реактивов
4319	3	8	5	4.00	2026-05-18 15:07:10.78	Массовое редактирование реактивов
4320	2	8	5	5.50	2026-05-18 15:10:00.05	Массовое редактирование реактивов
4321	8	8	5	8.00	2026-05-18 15:11:21.43	Массовое редактирование реактивов
4322	8	8	5	7.00	2026-05-18 15:12:48.83	Массовое редактирование реактивов
4323	3	8	5	3.00	2026-05-18 15:24:42.47	Массовое редактирование реактивов
4324	5	8	5	9.00	2026-05-18 17:10:23.33	Массовое редактирование реактивов
4325	5	8	5	8.00	2026-05-18 17:11:05.41	Массовое редактирование реактивов
4326	8	8	5	6.00	2026-05-18 17:11:05.61	Массовое редактирование реактивов
4327	3	8	5	2.00	2026-05-18 17:11:05.62	Массовое редактирование реактивов
4328	11	8	5	6.00	2026-05-18 17:11:05.65	Массовое редактирование реактивов
4329	2	8	5	4.50	2026-05-18 17:11:05.65	Массовое редактирование реактивов
4330	5	8	5	7.00	2026-05-18 17:13:54.43	Массовое редактирование реактивов
4331	8	8	5	5.00	2026-05-18 17:13:54.45	Массовое редактирование реактивов
4332	3	8	5	1.00	2026-05-18 17:13:54.45	Массовое редактирование реактивов
4333	11	8	5	5.00	2026-05-18 17:13:54.48	Массовое редактирование реактивов
4334	2	8	5	3.50	2026-05-18 17:13:54.49	Массовое редактирование реактивов
4335	1	8	3	0.00	2026-05-18 17:25:01.21	Мягкое удаление (архивация)
4336	2	8	3	0.00	2026-05-18 17:25:01.24	Мягкое удаление (архивация)
4337	3	8	3	0.00	2026-05-18 17:25:01.24	Мягкое удаление (архивация)
4338	3	8	5	0.00	2026-05-18 17:25:07.11	Восстановление данных
4339	2	8	5	0.00	2026-05-18 17:25:08.69	Восстановление данных
4340	1	8	5	0.00	2026-05-18 17:25:10.73	Восстановление данных
4341	11	8	5	4.00	2026-05-18 17:25:24.96	Массовое редактирование реактивов
4342	5	8	5	6.00	2026-05-18 17:25:24.95	Массовое редактирование реактивов
4343	8	8	5	4.00	2026-05-18 17:25:24.95	Массовое редактирование реактивов
4344	5	8	5	5.00	2026-05-18 17:26:27.56	Массовое редактирование реактивов
4345	8	8	5	3.00	2026-05-18 17:26:27.57	Массовое редактирование реактивов
4346	5	8	5	4.00	2026-05-18 17:39:45.89	Массовое редактирование реактивов
4347	8	8	5	2.00	2026-05-18 17:39:45.94	Массовое редактирование реактивов
4348	11	8	5	3.00	2026-05-18 17:39:45.95	Массовое редактирование реактивов
4349	5	8	5	14.00	2026-05-18 17:40:37.26	Массовое редактирование реактивов
4350	8	8	5	12.00	2026-05-18 17:40:37.26	Массовое редактирование реактивов
4351	11	8	5	13.00	2026-05-18 17:40:37.26	Массовое редактирование реактивов
4352	3	8	5	6.00	2026-05-18 17:45:02.36	Массовое редактирование реактивов
4353	11	8	5	12.00	2026-05-18 17:48:15.12	Массовое редактирование реактивов
4354	9	8	5	4.50	2026-05-18 17:48:15.12	Массовое редактирование реактивов
4355	25	8	5	3.00	2026-05-18 17:48:15.12	Массовое редактирование реактивов
4356	62	8	5	14.00	2026-05-18 17:48:15.13	Массовое редактирование реактивов
4357	16	8	5	0.00	2026-05-18 17:51:46.8	Восстановление данных
4358	15	8	5	0.00	2026-05-18 17:51:48.32	Восстановление данных
4359	14	8	5	0.00	2026-05-18 17:51:49.87	Восстановление данных
4360	13	8	5	0.00	2026-05-18 17:51:51.31	Восстановление данных
4361	15	8	5	-1.00	2026-05-18 17:51:57.94	Массовое редактирование реактивов
4362	13	8	5	14.00	2026-05-18 17:51:57.94	Массовое редактирование реактивов
4363	1	8	5	9.00	2026-05-18 17:53:11.09	Массовое редактирование реактивов
4364	3	8	5	0.00	2026-05-18 17:53:11.14	Массовое редактирование реактивов
4365	1	8	5	19.00	2026-05-20 12:55:25.56	Массовое редактирование реактивов
4366	2	8	5	13.50	2026-05-20 12:55:25.63	Массовое редактирование реактивов
4367	3	8	5	15.00	2026-05-20 13:01:07.79	Массовое редактирование реактивов
4368	25	8	5	9.00	2026-05-20 13:33:15.63	Массовое редактирование реактивов
4369	32	8	5	10.00	2026-05-20 13:33:15.64	Массовое редактирование реактивов
4370	8	8	5	15.00	2026-05-21 08:53:12.64	Массовое редактирование реактивов
4371	5	8	5	24.50	2026-05-21 08:55:39.26	Массовое редактирование реактивов
4372	2	8	5	13.50	2026-05-21 09:13:34.57	Редактирование параметров реактива
4373	3	8	5	15.00	2026-05-21 09:14:10.73	Редактирование параметров реактива
4374	8	8	5	15.00	2026-05-21 09:19:39.14	Редактирование параметров реактива
4375	9	8	5	4.50	2026-05-21 09:19:59.18	Редактирование параметров реактива
4376	11	8	5	12.00	2026-05-21 09:20:35.25	Редактирование параметров реактива
4377	12	8	5	0.00	2026-05-21 09:21:33.91	Восстановление данных
4378	12	8	5	60.00	2026-05-21 09:22:00.52	Редактирование параметров реактива
4379	12	8	3	0.00	2026-05-21 09:22:03.47	Мягкое удаление (архивация)
4380	3	8	5	5.00	2026-05-21 14:57:21	Массовое редактирование реактивов
4381	5	8	5	14.50	2026-05-21 14:57:21.04	Массовое редактирование реактивов
4382	8	8	5	5.00	2026-05-21 14:58:05.36	Массовое редактирование реактивов
4383	10	8	5	5.00	2026-05-21 14:58:05.37	Массовое редактирование реактивов
4384	8	8	5	4.00	2026-05-21 15:02:17.79	Массовое редактирование реактивов
4385	10	8	5	4.00	2026-05-21 15:02:17.8	Массовое редактирование реактивов
4386	8	8	5	3.00	2026-05-21 15:04:10.81	Массовое редактирование реактивов
4387	10	8	5	3.00	2026-05-21 15:04:10.82	Массовое редактирование реактивов
4388	3	8	5	4.00	2026-05-21 15:04:10.82	Массовое редактирование реактивов
4389	2	8	5	8.50	2026-05-21 15:04:10.82	Массовое редактирование реактивов
4390	2	8	5	25.50	2026-05-21 19:23:30.88	Массовое редактирование реактивов
4391	5	8	5	25.00	2026-05-21 19:23:30.99	Массовое редактирование реактивов
4392	4	8	5	144.00	2026-05-21 19:23:30.99	Массовое редактирование реактивов
4393	8	8	5	6.00	2026-05-22 16:57:01.96	Массовое редактирование реактивов
4394	2	8	5	42.50	2026-05-22 16:57:02.22	Массовое редактирование реактивов
4395	5	8	5	35.50	2026-05-22 16:57:02.25	Массовое редактирование реактивов
4396	8	8	5	6.00	2026-05-22 16:57:21.26	Массовое редактирование реактивов
4397	2	8	5	42.50	2026-05-22 16:57:21.27	Массовое редактирование реактивов
4398	5	8	5	35.50	2026-05-22 16:57:21.27	Массовое редактирование реактивов
4399	8	8	5	6.00	2026-05-22 16:59:58.06	Массовое редактирование реактивов
4400	2	8	5	42.50	2026-05-22 16:59:58.06	Массовое редактирование реактивов
4401	5	8	5	35.50	2026-05-22 16:59:58.06	Массовое редактирование реактивов
4402	8	8	5	9.00	2026-05-22 17:01:49.31	Массовое редактирование реактивов
4403	2	8	5	59.50	2026-05-22 17:01:49.31	Массовое редактирование реактивов
4404	5	8	5	46.00	2026-05-22 17:01:49.31	Массовое редактирование реактивов
4405	2	8	5	76.50	2026-05-22 17:20:05.88	Массовое редактирование реактивов
4406	5	8	5	56.50	2026-05-22 17:20:05.88	Массовое редактирование реактивов
4407	4	8	5	156.00	2026-05-22 17:20:05.88	Массовое редактирование реактивов
4408	8	8	5	12.00	2026-05-22 17:20:21.26	Массовое редактирование реактивов
4409	8	8	5	15.00	2026-05-22 17:20:49.12	Массовое редактирование реактивов
4410	8	8	5	25.00	2026-05-22 17:23:43.12	Массовое редактирование реактивов
4411	10	8	5	13.00	2026-05-22 17:29:04.98	Массовое редактирование реактивов
4412	63	8	1	0.00	2026-05-22 18:17:03.84	Операция Receipt над реагентом d
4413	1	8	5	10.00	2026-05-23 18:18:47.67	Массовое редактирование реактивов
4414	4	8	5	166.00	2026-05-24 13:04:54.83	Массовое редактирование реактивов
4415	16	8	5	50.00	2026-05-24 13:04:54.92	Массовое редактирование реактивов
4416	4	8	3	0.00	2026-05-24 13:05:04.67	Мягкое удаление (архивация)
4417	16	8	3	0.00	2026-05-24 13:05:06.91	Мягкое удаление (архивация)
4418	4	8	5	0.00	2026-05-24 13:05:09.08	Восстановление данных
4419	16	8	5	0.00	2026-05-24 13:05:11.21	Восстановление данных
4420	4	8	5	156.00	2026-05-24 13:05:28.49	Массовое редактирование реактивов
4421	16	8	5	45.00	2026-05-24 13:05:28.5	Массовое редактирование реактивов
4422	25	8	5	-1.00	2026-05-24 13:05:28.5	Массовое редактирование реактивов
4423	5	8	5	46.50	2026-05-24 13:05:28.5	Массовое редактирование реактивов
4424	2	8	5	93.50	2026-05-24 13:05:54.78	Массовое редактирование реактивов
4425	5	8	5	59.50	2026-05-24 13:05:54.78	Массовое редактирование реактивов
4426	4	8	5	168.00	2026-05-24 13:05:54.78	Массовое редактирование реактивов
4427	2	8	5	11.50	2026-05-24 13:13:07.83	Массовое редактирование реактивов
4428	2	8	5	15.00	2026-05-24 13:13:15.64	Массовое редактирование реактивов
4429	3	8	5	12.00	2026-05-24 13:13:31.14	Массовое редактирование реактивов
4430	1	8	5	15.00	2026-05-24 13:13:37.73	Массовое редактирование реактивов
4431	2	8	5	16.00	2026-05-24 13:17:21	Массовое редактирование реактивов
4432	25	8	5	15.00	2026-05-24 13:50:13.64	Массовое редактирование реактивов
4433	16	8	5	-108.00	2026-05-24 13:50:33.97	Массовое редактирование реактивов
4434	4	8	5	133.00	2026-05-24 13:50:33.97	Массовое редактирование реактивов
4435	16	8	5	15.00	2026-05-24 13:51:17.15	Массовое редактирование реактивов
4436	1	8	3	0.00	2026-05-24 13:53:50.05	Мягкое удаление (архивация)
4437	2	8	3	0.00	2026-05-24 13:53:50.05	Мягкое удаление (архивация)
4438	3	8	3	0.00	2026-05-24 13:53:50.05	Мягкое удаление (архивация)
4439	1	8	5	0.00	2026-05-24 13:53:57.5	Восстановление данных
4440	2	8	5	0.00	2026-05-24 13:53:59.89	Восстановление данных
4441	3	8	5	0.00	2026-05-24 13:54:02.09	Восстановление данных
4442	1	8	5	5.00	2026-05-24 13:54:11.23	Массовое редактирование реактивов
4443	2	8	5	6.00	2026-05-24 13:54:11.24	Массовое редактирование реактивов
4444	3	8	5	2.00	2026-05-24 13:54:11.25	Массовое редактирование реактивов
4445	2	8	5	23.00	2026-05-24 13:54:52.82	Массовое редактирование реактивов
4446	5	8	5	70.00	2026-05-24 13:54:52.83	Массовое редактирование реактивов
4447	3	8	5	12.00	2026-05-24 15:45:56.74	Массовое редактирование реактивов
4448	1	8	5	15.00	2026-05-24 15:45:56.76	Массовое редактирование реактивов
4449	2	8	5	16.00	2026-05-24 15:46:19.41	Массовое редактирование реактивов
\.


--
-- TOC entry 4835 (class 0 OID 122770)
-- Dependencies: 227
-- Data for Name: "ReagentReceipts" ; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."ReagentReceipts"  ("Id", "ReagentId", "SupplierId", "Quantity", "ReceiptDate", "DocumentNumber") FROM stdin;
1	1	1	10.00	2025-01-10	INV-001
2	3	2	5.00	2025-01-15	INV-002
3	4	1	20.00	2025-02-01	INV-003
\.


--
-- TOC entry 4831 (class 0 OID 122752)
-- Dependencies: 223
-- Data for Name: reagents; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Reagents"  ("Id", "Name", "ChemicalFormula", "Unit", "CurrentQuantity", "MinQuantity", "ExpirationDate", "StorageLocation", "CategoryId", "CreatedAt", "IsActive", "DeletedAt") FROM stdin;
1	Серная кислота	H2SO4	мл	15.00	10.00	2026-03-26	Склад A1	5	0001-01-01 00:00:00	true	\N
2	Соляная кислота	NaOH	л	16.00	10.00	2026-12-31	Склад Б, полка 4	1	2025-01-10 14:30:00	true	\N
3	Нитрат серебра	AgNO3	кг	12.00	5.00	2027-01-01	Склад B1	2	2026-01-17 18:11:44.5	true	\N
4	Этанол	C2H5OH	л	133.00	15.00	2025-06-01	Склад C1	3	2026-01-17 18:11:44.5	true	\N
5	Хлорид натрия	NaCl	кг	70.00	20.00	2028-12-31	Склад D1	4	2026-01-17 18:11:44.5	true	\N
8	Перманганат калия	KMnO4	л	25.00	10.00	2026-03-01	Склад A1	1	0001-01-01 00:00:00	true	\N
9	Уксусная кислота	CH3COOH	л	4.50	10.00	2026-12-31	Склад Б, полка 4	1	2025-01-10 14:30:00	true	\N
10	Гидроксид натрия	NaOH	кг	13.00	5.00	2027-01-01	Склад B1	2	2026-01-17 18:11:44.5	true	\N
11	Аммиак	NH3 (H2O)	л	12.00	15.00	2025-06-01	Склад C1	3	0001-01-01 00:00:00	true	\N
12	Сульфат меди(II)	CuSO4	кг	60.00	20.00	2028-12-31	Склад D1	4	0001-01-01 00:00:00	false	2026-05-21 09:22:03.48
13	Серная кислота	H2SO4	л	14.00	10.00	2026-03-01	Склад A1	1	2026-01-17 18:11:44.5	true	\N
14	Серная кислота	H2SO4	л	25.50	10.00	2026-12-31	Склад Б, полка 4	1	2025-01-10 14:30:00	true	\N
15	Гидроксид натрия	NaOH	кг	-1.00	5.00	2027-01-01	Склад B1	2	2026-01-17 18:11:44.5	true	\N
16	Этанол	C2H5OH	л	15.00	15.00	2025-06-01	Склад C1	3	2026-01-17 18:11:44.5	true	\N
25	Хромиум	Cr	кг	15.00	6.00	2026-04-30	Особо безопасное место в лаборатории	8	0001-01-01 00:00:00	true	\N
30	Азотная кислота	HNO3	л	5.00	2.00	2027-05-20	Шкаф для кислот №1	1	2026-04-07 13:31:00.81	true	\N
31	Гидроксид калия	KOH	кг	2.50	0.50	2026-12-10	Стеллаж Б	2	2026-04-07 13:31:00.81	true	\N
32	Изопропиловый спирт	C3H8O	л	10.00	5.00	2028-01-15	Склад ЛВЖ	3	0001-01-01 00:00:00	true	\N
33	Сульфат меди(II)	CuSO4	кг	1.20	0.50	2029-03-10	Полка 4	4	2026-04-07 13:31:00.81	true	\N
34	Оксид марганца(IV)	MnO2	кг	0.80	0.20	2030-06-01	Стеллаж Г	5	2026-04-07 13:31:00.81	true	\N
35	Фенолфталеин	C20H14O4	г	500.00	100.00	2027-11-20	Шкаф индикаторов	6	2026-04-07 13:31:00.81	true	\N
36	Буферный раствор pH 4.01	\N	л	2.00	1.00	2025-09-14	Холодильник №2	7	2026-04-07 13:31:00.81	true	\N
37	Магниевая стружка	Mg	кг	0.30	0.10	2031-02-28	Сейф хран. металлов	8	2026-04-07 13:31:00.81	true	\N
38	Ацетон	CH3COCH3	л	15.00	5.00	2026-08-12	Склад ЛВЖ	3	2026-04-07 13:31:00.81	true	\N
39	Нитрат серебра	AgNO3	г	250.00	100.00	2027-04-05	Сейф №1	4	0001-01-01 00:00:00	true	\N
40	Сульфид хрома(III)	Cr2S3	г	250.00	50.00	2026-05-29	Сейф №1	4	2026-04-09 12:55:38.74	true	\N
41	Медь	Cu	г	250.00	50.00	2026-06-30	Сейф №1	8	2026-04-09 13:02:46.61	true	\N
50	Натрий хлор	NaCl	г	100.00	10.00	2028-04-14	Полка N1	4	2026-04-14 19:04:26.24	true	\N
51	Серная кислота	H2SO4	г	50.00	5.00	2026-04-09	Полка N1	1	2026-04-14 19:04:29.77	true	\N
60	Фенолфталеин	C20H14O4	кг	4.00	4.00	2026-04-23	2 Полка	7	2026-04-24 14:19:38.35	false	2026-05-13 18:10:05.69
61	Вода	H2O	л	89.00	5.00	2026-04-29	Холодильник №2	5	2026-04-24 19:56:10.84	false	\N
62	Новый реагент	CuOP5	л	14.00	10.00	2030-10-23	Шкаф	8	2026-04-28 18:37:12	true	\N
63	Серебро	Ag	кг	5.00	10.00	\N		2	2026-05-22 18:17:03.92	true	\N
\.


--
-- TOC entry 4828 (class 0 OID 122741)
-- Dependencies: 220
-- Data for Name: "Suppliers"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Suppliers" ("Id", "Name", "ContactInfo", "Address", "IsActive", "DeletedAt") FROM stdin;
1	ООО '1'	+375 29 259 65 98	ул. Нижегородская	true	\N
2	ООО '2'	+375 29 259 65 98	ул. Нижегородская	false	2026-04-26 20:20:01.39
11	Поставщик 1	+659656985622	Новая 34	true	\N
\.


--
-- TOC entry 4829 (class 0 OID 122746)
-- Dependencies: 221
-- Data for Name: "SystemRoles"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."SystemRoles" ("Id", "Name", "DisplayName") FROM stdin;
1	User	Пользователь
2	Admin	Администратор
3	Super_Admin	Супер-администратор
\.


--
-- TOC entry 4832 (class 0 OID 122755)
-- Dependencies: 224
-- Data for Name: "Users"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Id","IdWorkSchedule", "FirstName", "MiddleName", "LastName", "Email", "Sex", "SystemRoleId", "JobPosition", "Login", "PasswordHash", "IsActive", "DeletedAt") FROM stdin;
5	1	Иван	Иванович	Петров	simpleadmin@gmail.com	M	2	Заведующий лабораторией	admin	vtzIXpUOBhZTDGfG5ieEVg==:JeldURBzUEgN9gwWWGotdJV/MrSFtKOiz2N51LE3C/M=	true	\N
6	2	Анна	Сергеевна	Ковалёва	alena@lab.ru	F	1	Лаборант	anna	HASH_USER	true	\N
7	1	Дмитрий	Олегович	Смирнов	simpleuser@galaktika.ru	M	1	Химик-аналитик	user	Bp/2NiLSoIbpY0fKrWITKw==:QjOjcCjbhvNZgMEUyI/UdkOre5Y/avE/sLt6e8sIuWY=	true	\N
8	1	Дмитрий	Михайлович	Василевич	superadmin@gmail.com	M	3	Химик-аналитик	superadmin	0igZeV+//l7+BFSURz4fOA==:jUCoEo/pqBFGpUKz4QtAEA5a9Mkk6/mhdYHmrFHog3I=	true	\N
\.


--
-- TOC entry 4830 (class 0 OID 122749)
-- Dependencies: 222
-- Data for Name: "WorkSchedule"; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."WorkSchedule" ("Id", "WorkShift", "StartTime", "EndTime") FROM stdin;
1	Утренняя	08:00:00	16:00:00
2	Вечерняя	16:00:00	00:00:00
3	Ночная	00:00:00	08:00:00
\.


-- Completed on 2026-08-12 20:29:18

--
-- PostgreSQL database dump complete
--

-- Completed on 2026-08-12 20:29:18

--
-- PostgreSQL database cluster dump complete
--

