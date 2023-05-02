
# Ília - Desafio Ília

Projeto para testar o conhecimento sobre exposição de API.

Detalhamento do desafio:
[DesafioIlia](https://github.com/IAmHopp/desafio-ilia)



## Ambiente de desenvolvimento

#### Requerido
 - Visual Studio ou Visual Code.
 - Framework .NET 7.
 - Docker.

#### 1. Docker
Instalar banco de dados PostgreSQL
```bash
docker pull postgres
```

Iniciar instância PostgreSQL
```bash
docker run --name ilia -e POSTGRES_PASSWORD=0b2c539c3e7f85d808df3f2dfe8906b9 -p 5432:5432 -d postgres
```

#### 2. Repositório

Realizar clone do projeto.
```bash
git clone https://------/
```
    
## API Reference

#### Get all items

```http
  POST /v1/batidas
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `dia` | `string` | **Obrigatório**. Dia no formato 2023-04-30. |
| `horarios` | `array(string)` | **Obrigatório**. Lista com horários no formato "18:00:00" cada. |

#### Get item

```http
  GET /v1/folhas-de-ponto/{mes}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `mes`      | `string` | **Obrigatório**. Mês e ano no formato "2018-08" |

