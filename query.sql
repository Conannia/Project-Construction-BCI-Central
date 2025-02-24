
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
);


INSERT INTO users (id, email, password) VALUES
(1, 'admin@example.com', '123456'),
(2, 'test', 'test'),
(3, 'string', 'string');


CREATE TABLE projects (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    location VARCHAR(500) NOT NULL,
    stage ENUM('Concept', 'Design', 'Pre-Construction', 'Construction', 'Completed') NOT NULL,
    category VARCHAR(255) NOT NULL,
    startDate DATE NOT NULL,
    details TEXT NOT NULL,
    creatorId VARCHAR(255) NOT NULL
);


INSERT INTO projects (id, name, location, stage, category, startDate, details, creatorId) VALUES
(100003, 'Test Edit part 3', 'string', 'Pre-Construction', 'Health', '2025-02-28', 'string ini test', 'string'),
(159984, 'Test 2', 'Jakarta', 'Concept', 'Education', '2025-02-28', 'ini test', 'qwqw'),
(211965, 'asa', 'asas', 'Pre-Construction', 'asa', '2025-02-27', 'asa', '122'),
(245118, 'ini test setelah connect API', 'INI TEST', 'Concept', 'Education', '2025-02-27', 'INI TEST', 'TEST'),
(350755, 'hgh', 'hh', 'Concept', 'Education', '2025-02-28', 'hjh', '2'),
(367155, 'Ini test Tapi saya tidak Paham', 'Ini test', 'Concept', 'Education', '2025-02-28', 'Ga ngerti gw', 'Ini test'),
(367156, 'string', 'string', 'Pre-Construction', 'Office', '2025-02-23', 'string', 'string'),
(836711, 'test baru', 'ini test lokasi', 'Concept', 'Education', '2025-02-12', 'jj', '2');
