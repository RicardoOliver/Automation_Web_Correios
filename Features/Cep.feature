
@correios
Feature: Consulta nos Correios, onde devemos inserir cep que não existe,cep que existe e rastreamento.

Scenario Outline: Correios
	Given que estou no site dos Correios
	When eu procuro pelo CEP que não existe '<cepError>' 
	Then o CEP pesquisado não deve existir
	And devo voltar para a tela inicial

	When eu procuro pelo CEP '<cepCorrect>'
	Then o resultado deve ser '<resultado>'
	And eu volto para a tela inicial

	Given que estou no site dos Correios de Rastreio
	When eu procuro pelo código de rastreamento '<rastreio>'
	Then o código não está correto '<codigo>'

    Then eu fecho o navegador

Examples:
| cepError | cepCorrect | resultado                            | rastreio      | codigo|
| 80700000 | 01013-001  | Rua Quinze de Novembro, São Paulo/SP | SS987654321BR | Objeto não localizado! |
