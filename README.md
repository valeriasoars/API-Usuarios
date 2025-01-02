# API CRUD com Autenticação

A API oferece um CRUD completo com suporte à autenticação, garantindo segurança e controle de acesso às funcionalidades.

## Estrutura da API

### Controlador de Login
- **Endpoints**:
  - **Registro de novos usuários**
  - **Autenticação (login)**
    
### Controlador de Usuários
- **Endpoints**:
  - **Listar usuários por ID específico**
  - **Listar todos os usuários**
  - **Editar informações de um usuário existente**
  - **Remover um usuário**

## Segurança

Os endpoints do controlador de usuários são protegidos por autenticação. O acesso só é permitido mediante a inclusão de um token de autenticação no cabeçalho das requisições, utilizando o formato Bearer {token}.
Essa abordagem garante que apenas usuários autenticados possam realizar operações sensíveis na aplicação.

