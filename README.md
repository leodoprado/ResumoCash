<img width="1805" height="324" alt="Group 19" src="https://github.com/user-attachments/assets/dc05f8b4-4fe7-4da3-815c-b6f3e68fcd80" />

# Arquitetura da Aplicação

Para o desenvolvimento da aplicação, foi adotada a Clean Architecture (Arquitetura Limpa), uma abordagem de organização de software que busca separar as responsabilidades do sistema em diferentes camadas, promovendo baixo acoplamento, alta coesão, facilidade de manutenção, testabilidade e escalabilidade.

A solução está organizada em quatro projetos principais:
- ResumoCash.Api
- ResumoCash.Application
- ResumoCash.Domain
- ResumoCash.Infrastructure

## ResumoCash.Api

Responsável pela camada de apresentação e comunicação externa da aplicação. Sua principal função é disponibilizar os endpoints HTTP que permitem interação entre o cliente e o sistema, recebendo requisições, encaminhando-as para a camada de aplicação e retornando as respostas apropriadas. Essa camada deve evitar a implementação direta de regras de negócio, delegando essas responsabilidades para as camadas apropriadas.

## ResumoCash.Application

Responsável pela implementação e orquestração dos casos de uso da aplicação. Essa camada estabelece como as funcionalidades do sistema devem ser executadas, coordenando as entidades de domínio e utilizando abstrações para acessar recursos externos, como bancos de dados e serviços. Essa camada depende do Domain, mas não deve depender diretamente da insfraestrutura ou das tecnologias de persistência.

## ResumoCash.Domain

Representa o núcleo da aplicação, concentrando as entidades e regras de negócio fundamentais do sistema. Essa camada deve ser independente das demais, garantindo que a lógica de negócio não fique vinculada a frameworks, banco de dados ou mecanismos de comunicação externos. Por ser a camada mais interna da arquitetura, o Domain não deve possuir dependências dos demais projetos da solução.

## ResumoCash.Infrastructure

Responsável pela implementação dos recursos técnicos e mecanismos de acesso a serviços externos utilizados pela aplicação. Essa camada fornece as implementações concretas das abstrações definidas nas camadas internas, permitindo que os casos de uso sejam executados sem conhecer os detalhes técnicos envolvidos. Dessa forma, a camada de infraestrutura pode ser modificada ou substituída com impacto reduzido sobre as regras de negócio e os casos de uso.

<img width="1536" height="1024" alt="Untitled" src="https://github.com/user-attachments/assets/80eaeaba-cc29-4d46-b916-bd17270ba094" />
