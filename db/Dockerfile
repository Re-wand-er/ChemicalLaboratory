FROM postgres:17-alpine

COPY postgresql.conf /etc/postgresql/postgresql.conf

COPY init.sql /docker-entrypoint-initdb.d/

CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]
