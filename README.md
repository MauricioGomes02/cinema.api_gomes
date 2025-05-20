Ol�, seja bem-vindo(a) ao cinema.api. Essa aplica��o � respons�vel por propor 
a estrutura��o e funcionamento da integra��o entre microservi�os, com o intuito 
de fornecer informa��es sobre filmes, usu�rios e suas rela��es.
A aplica��o tem como base em sua arquitetura a ideia de portas e seus adaptadores
, comumente conhecida como Ports and Adapters ou Hexagonal Architecture. A ideia
da arquitetura � realizar a invers�o de depend�ncia na tomada de decis�o das
implementa��es, criando assim contratos (portas) que se comunicam com o ambiente 
externo, podendo assumir diferentes implementa��es (adaptadores). Isso fornece 
maior facilidade em cria��o de testes e torna a aplica��o menos acoplada.

Al�m da estrutura��o de arquivos, temos tamb�m a arquitetura de microservi�os, 
respons�vel por propor a atomicidade das aplica��es e sua independ�ncia, evitando
que um problema em um determinado servi�o, causa consequ�ncias em algum outro, como
pode ocorrer em monolitos. 

Para essa aplica��o, foram separados 3 (tr�s) microservi�os, sendo eles:

- Filme
- Usu�rio
- Aluguel

Portanto, sempre que um usu�rio for criado, alterado ou exclu�do, o servi�o de
aluguel ser� notificado para manter a consist�ncia de informa��es. Observe o diagrama
abaixo para um melhor entendimento das rela��es.

```mermaid
---
title: cinema.api
---
erDiagram
    ALUGUEL }o--|{ FILME : possui
    USUARIO |{--o{ ALUGUEL : possui
```

A aplica��o contar� com 3 apis, uma para cada servi�o, como tamb�m de um sistema
de mensageria para comunica��o ass�ncrona e atrav�s de eventos.
