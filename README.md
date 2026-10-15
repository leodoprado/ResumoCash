<img width="1805" height="324" alt="Group 19" src="https://github.com/user-attachments/assets/dc05f8b4-4fe7-4da3-815c-b6f3e68fcd80" />

## Arquitetura do Backend

<img width="2790" height="2064" alt="mermaid-diagram(1)" src="https://github.com/user-attachments/assets/97066d0b-031f-457a-b20a-30d572651451" />

O backend do **ResumoCash** utiliza os princípios da **Clean Architecture**, organizando as responsabilidades em quatro camadas independentes, com o objetivo de garantir separação de responsabilidades, baixo acoplamento, testabilidade e facilidade de manutenção.

| Camada | Responsabilidade |
|---|---|
| **ResumoCash.Api** | Exposição de endpoints REST, recebimento de requisições HTTP, autenticação, autorização e retorno das respostas. |
| **ResumoCash.Application** | Orquestração dos casos de uso, validações de aplicação, comunicação com os contratos dos repositórios e definição de DTOs. |
| **ResumoCash.Domain** | Entidades, regras de negócio, invariantes, enums e contratos dos repositórios. Não depende das demais camadas. |
| **ResumoCash.Infrastructure** | Implementação de persistência utilizando Entity Framework Core, PostgreSQL, repositórios, mapeamentos e migrations. |

#### Fluxo de execução

1. O cliente realiza uma requisição HTTP para um endpoint.
2. A camada **Api** recebe a requisição e encaminha a operação para a **Application**.
3. A **Application** coordena o caso de uso, consultando repositórios e utilizando entidades do **Domain**.
4. O **Domain** protege as regras de negócio e garante a consistência das entidades.
5. A **Infrastructure** implementa os contratos de persistência e interage com o PostgreSQL.
6. O resultado retorna à camada **Api**, responsável por enviar a resposta HTTP ao cliente.

#### Princípio de dependência

As dependências de código seguem em direção às camadas internas. A camada **Domain** não depende de frameworks, infraestrutura ou mecanismos de apresentação.

A implementação dos repositórios ocorre na **Infrastructure**, enquanto seus contratos são definidos no **Domain**, respeitando o princípio de inversão de dependência (DIP).
