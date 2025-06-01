# Product Warranty Sales System

Este projeto é um sistema de microserviços desenvolvido em .NET 8, implementando diferentes padrões arquiteturais para demonstrar diferentes abordagens de design.

## Arquitetura

O sistema é composto por três microserviços:

### Product API (Clean Architecture)
- Gerenciamento de produtos
- Implementado usando Clean Architecture
- Separação clara entre camadas (Domain, Application, Infrastructure, API)

### Warranty API (Clean Architecture)
- Gerenciamento de garantias
- Implementado usando Clean Architecture
- Separação clara entre camadas (Domain, Application, Infrastructure, API)

### Sale API (Vertical Slice Architecture)
- Gerenciamento de vendas
- Implementado usando Vertical Slice Architecture
- Organização por features/funcionalidades
- Cada feature é autocontida com seus próprios modelos, handlers e validações

## Como Executar

### Pré-requisitos
- Docker
- .NET 8 SDK
- Visual Studio 2022 ou JetBrains Rider

### Usando Docker
# Clone o repositório
git clone [url-do-repositório]
# Navegue até a pasta do projeto
cd ProductWarrantySales
# Build e execução dos containers
docker-compose up --build
### Portas dos Serviços
- Warranty API: http://localhost:8081
- Sale API: http://localhost:8082
- Product API: http://localhost:8083

## Testando as APIs

### Usando Arquivos HTTP

O projeto inclui arquivos `.http` para testar as APIs diretamente do Visual Studio Code ou JetBrains Rider. Estes arquivos estão localizados em:
