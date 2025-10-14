-- Usar o banco de dados recém-criado
USE sistema_diario_leitura;

-- 1. Inserir dados na tabela 'status_leitura'
-- Estes são os status básicos que um livro pode ter na estante do usuário.
INSERT INTO status_leitura (nome) VALUES
('Lido'),
('Lendo'),
('Quero Ler');

-- 2. Inserir dados na tabela 'genero'
-- Alguns gêneros literários comuns para categorizar os livros.
INSERT INTO genero (nome) VALUES
('Ficção Científica'),
('Fantasia'),
('Romance'),
('Distopia'),
('Clássico'),
('Mistério');

-- 3. Inserir dados na tabela 'usuario'
-- Criação de alguns usuários de exemplo. Em um sistema real, a senha seria criptografada.
INSERT INTO usuario (nome, email, senha) VALUES
('Ana Silva', 'ana.silva@exemplo.com', 'senha123criptografada'),
('Bruno Costa', 'bruno.c@exemplo.com', 'senha456criptografada'),
('Carla Dias', 'carla.dias@exemplo.com', 'senha789criptografada');

-- 4. Inserir dados na tabela 'autor'
-- Adição de alguns autores conhecidos.
INSERT INTO autor (nome, data_nascimento, biografia) VALUES
('George Orwell', '1903-06-25', 'Eric Arthur Blair, mais conhecido pelo pseudônimo George Orwell, foi um romancista, ensaísta e jornalista inglês.'),
('J.R.R. Tolkien', '1892-01-03', 'John Ronald Reuel Tolkien foi um escritor, professor universitário e filólogo britânico, autor de O Senhor dos Anéis.'),
('Isaac Asimov', '1920-01-02', 'Isaac Asimov foi um escritor e bioquímico americano, conhecido por suas obras de ficção científica e divulgação científica.'),
('Jane Austen', '1775-12-16', 'Jane Austen foi uma romancista inglesa proeminente, cujas obras marcam a transição para o realismo do século XIX.'),
('Machado de Assis', '1839-06-21', 'Considerado o maior nome da literatura brasileira, escreveu romances, contos, poesias e crônicas.');

-- 5. Inserir dados na tabela 'livro'
-- Adição de livros dos autores acima.
INSERT INTO livro (titulo, ano_publicacao) VALUES
('1984', 1949),
('A Sociedade do Anel', 1954),
('Eu, Robô', 1950),
('Orgulho e Preconceito', 1813),
('Dom Casmurro', 1899);

-- 6. Inserir dados nas tabelas de relacionamento ('livro_autor' e 'livro_genero')
-- Conectando livros aos seus autores e gêneros.
-- A ordem dos IDs corresponde à ordem das inserções anteriores (ex: livro com id=1 é '1984', autor com id=1 é 'George Orwell').

-- Relacionando Livros com Autores
INSERT INTO livro_autor (id_livro, id_autor) VALUES
(1, 1), -- 1984 -> George Orwell
(2, 2), -- A Sociedade do Anel -> J.R.R. Tolkien
(3, 3), -- Eu, Robô -> Isaac Asimov
(4, 4), -- Orgulho e Preconceito -> Jane Austen
(5, 5); -- Dom Casmurro -> Machado de Assis

-- Relacionando Livros com Gêneros
INSERT INTO livro_genero (id_livro, id_genero) VALUES
(1, 4), -- 1984 -> Distopia
(1, 1), -- 1984 -> Ficção Científica
(2, 2), -- A Sociedade do Anel -> Fantasia
(3, 1), -- Eu, Robô -> Ficção Científica
(4, 3), -- Orgulho e Preconceito -> Romance
(4, 5), -- Orgulho e Preconceito -> Clássico
(5, 5), -- Dom Casmurro -> Clássico
(5, 3); -- Dom Casmurro -> Romance

-- 7. Inserir dados na tabela 'leitura'
-- Criação dos registros de leitura para os usuários, simulando um diário.
-- id_status_leitura: 1='Lido', 2='Lendo', 3='Quero Ler'

INSERT INTO leitura (id_usuario, id_livro, id_status_leitura, nota, comentario, data_inicio, data_fim) VALUES
-- Leituras da Ana Silva (id_usuario = 1)
(1, 1, 1, 5, 'Um clássico distópico atemporal. Assustadoramente relevante.', '2024-01-10', '2024-01-25'),
(1, 2, 2, NULL, 'Iniciando a jornada pela Terra-média!', '2024-02-01', NULL),

-- Leituras do Bruno Costa (id_usuario = 2)
(2, 5, 1, 4, 'Final ambíguo que me deixou pensando por dias. Capitu traiu ou não?', '2024-03-15', '2024-03-30'),
(2, 3, 3, NULL, 'Recomendação de um amigo, parece interessante.', NULL, NULL),

-- Leituras da Carla Dias (id_usuario = 3)
(3, 4, 1, 5, 'Um dos meus romances favoritos de todos os tempos. Mr. Darcy é inesquecível.', '2024-05-20', '2024-06-05');