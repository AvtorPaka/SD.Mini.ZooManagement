## Software Design || HSE-SE 2 course

**Демченко Георгий Павлович, БПИ-235**

### Boot guidance

#### Change .env.template variables if needed

| **Variable**        | **Description** | **Default** |
|---------------------|--------------|-------------|
| **SD_ZOO_API_PORT** | Port, forwarded to docker container with application | 7070        |
| **BUILD_ARCH**      | Your system / docker builder architecture | arm64       |
| **BUILD_PLATFORM**  | Your system / docker builder OS  | linux/arm64 |

#### Program boot

```shell
cd SD.Mini.ZooManagement/ && touch .env && cp .env.template .env && docker compose up -d
```

### Swagger

Swagger will be available for address:  **http://localhost:{SD_ZOO_API_PORT}/api/sd-zoo/swagger/**

### Implemented functionality

All implemented functionality is reflected in Swagger. 


### Concepts and principles of DDD and Clean Architecture