CREATE DATABASE IF NOT EXISTS dev;

CREATE USER 'shogunskinoski'@'localhost' IDENTIFIED BY 'nevarlanteneke123.';
GRANT ALL PRIVILEGES ON dev.* TO 'shogunskinoski'@'localhost';
FLUSH PRIVILEGES;