Use ChamaJussa
GO

INSERT INTO localizacao (nome, andar)
VALUES 
	('Sala do Diretor', 'Térreo'), 
	('Sala da Coordenação da Faculdade', 'Térreo'),
	('Sala de Reunião', 'Térreo'),
	('Secretaria', 'Térreo'),
	('Biblioteca', 'Térreo'),
	('Copa dos Funcionários', 'Térreo'),
	('Atendimento', 'Térreo'),
	('Sala 1', '1º Andar'),
	('Sala 2', '1º Andar'),
	('Sala 3', '1º Andar'),
	('Sala 04/05', '1º Andar'),
	('Sala 06/07', '1º Andar'),
	('Studio', '1º Andar'),
	('Mesacast', '1º Andar'); 
GO

INSERT INTO StatusItem (nomeStatus) 
VALUES 
	('Aberto'),
	('Em andamento'),
	('Concluído'),
	('Cancelado')
GO