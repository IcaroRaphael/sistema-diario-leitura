DROP DATABASE IF EXISTS sistema_diario_leitura;

CREATE DATABASE sistema_diario_leitura;

USE sistema_diario_leitura;

CREATE TABLE usuario (
    id INT AUTO_INCREMENT NOT NULL,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(320) NOT NULL,
    senha VARCHAR(255) NOT NULL,
    data_cadastro DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT pk_usuario PRIMARY KEY (id),
    CONSTRAINT uk_usuario_email UNIQUE KEY (email)
);

CREATE TABLE status_leitura (
    id TINYINT AUTO_INCREMENT NOT NULL,
    nome VARCHAR(20) NOT NULL,
    
    CONSTRAINT pk_status_leitura PRIMARY KEY (id),
    CONSTRAINT uk_status_leitura_nome UNIQUE KEY (nome)
);

CREATE TABLE autor (
    id INT AUTO_INCREMENT NOT NULL,
    nome VARCHAR(100) NOT NULL,
    data_nascimento DATE NOT NULL,
    biografia TEXT NULL,
    
    CONSTRAINT pk_autor PRIMARY KEY (id)
);

CREATE TABLE genero (
    id TINYINT AUTO_INCREMENT NOT NULL,
    nome VARCHAR(50) NOT NULL,
    
    CONSTRAINT pk_genero PRIMARY KEY (id),
    CONSTRAINT uk_genero_nome UNIQUE KEY (nome)
);

CREATE TABLE livro (
    id INT AUTO_INCREMENT NOT NULL,
    titulo VARCHAR(255) NOT NULL,
    ano_publicacao SMALLINT NOT NULL,
    
    CONSTRAINT pk_livro PRIMARY KEY (id)
);

CREATE TABLE livro_genero (
	id_livro INT NOT NULL,
    id_genero TINYINT NOT NULL,
    
    CONSTRAINT pk_livro_genero PRIMARY KEY (id_livro, id_genero),
	CONSTRAINT fk_livro_genero_livro FOREIGN KEY (id_livro) REFERENCES livro (id) ON DELETE CASCADE,
    CONSTRAINT fk_livro_genero_genero FOREIGN KEY (id_genero) REFERENCES genero (id)
);

CREATE TABLE livro_autor (
	id_livro INT NOT NULL,
    id_autor INT NOT NULL,
    
    CONSTRAINT pk_livro_autor PRIMARY KEY (id_livro, id_autor),
    CONSTRAINT fk_livro_autor_livro FOREIGN KEY (id_livro) REFERENCES livro (id) ON DELETE CASCADE,
    CONSTRAINT fk_livro_autor_autor FOREIGN KEY (id_autor) REFERENCES autor (id)
);

CREATE TABLE leitura (
    id INT AUTO_INCREMENT NOT NULL,
    id_usuario INT NOT NULL,
    id_status_leitura TINYINT NOT NULL,
    id_livro INT NOT NULL,
    nota TINYINT NULL,
    comentario VARCHAR(500) NULL,
    data_inicio DATE NULL,
    data_fim DATE NULL,
    
    CONSTRAINT pk_leitura PRIMARY KEY (id),
    CONSTRAINT fk_leitura_usuario FOREIGN KEY (id_usuario) REFERENCES usuario (id) ON DELETE CASCADE,
    CONSTRAINT fk_leitura_status_leitura FOREIGN KEY (id_status_leitura) REFERENCES status_leitura (id),
    CONSTRAINT fk_leitura_livro FOREIGN KEY (id_livro) REFERENCES livro (id),
    CONSTRAINT ck_leitura_nota CHECK (nota IS NULL OR (nota BETWEEN 1 AND 5)),
    CONSTRAINT ck_leitura_datas_validas CHECK (data_fim IS NULL OR data_fim >= data_inicio)
);
