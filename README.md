# PatternsInt-SAP_ESP

Simplified copy trading platform backend for the SAP / Enterprise Systems Patterns task.

## Overview

This repository contains a microservice-based simulation of a copy trading backend.

The system includes:

- Trader Service
- Subscription Service
- Accounting Service
- Exchange Proxy Service
- Copy Engine Service
- Shared Contracts
- Shared Infrastructure

## Architecture

Trader Service receives and stores trader signals. Subscription Service stores follower subscriptions. Copy Engine Service simulates a saga that copies trades for followers. Exchange Proxy Service simulates exchange execution. Accounting Service simulates balances, positions, and ledger entries.

## Patterns Demonstrated

- Microservices
- Outbox Pattern
- Saga Pattern
- Event Sourcing concept
- CQRS concept
- Exchange Proxy isolation
- Docker-based infrastructure preparation

See:

- `docs/Architecture.md`
- `docs/Patterns.md`
- `docs/TestingAndResults.md`

## Build

```powershell
dotnet restore .\PatternsInt-SAP_ESP.sln
dotnet build .\PatternsInt-SAP_ESP.sln
```

## Run Services

Open separate PowerShell windows and run:

```powershell
dotnet run --project .\src\SubscriptionService.Api
```

```powershell
dotnet run --project .\src\AccountingService.Api
```

```powershell
dotnet run --project .\src\ExchangeProxyService.Api
```

```powershell
dotnet run --project .\src\CopyEngineService.Worker
```

Optional Trader Service:

```powershell
dotnet run --project .\src\TraderService.Api
```

## Default Local Ports

| Service | URL |
|---|---|
| Trader Service | `http://localhost:5031` |
| Subscription Service | `http://localhost:5224` |
| Accounting Service | `http://localhost:5222` |
| Exchange Proxy Service | `http://localhost:5021` |

## Smoke Test

After starting Subscription, Accounting, and Exchange Proxy services, run:

```powershell
.\scripts\smoke-test.ps1
```

## Load Testing

Install Locust:

```powershell
pip install locust
```

Run Subscription Service load test:

```powershell
locust -f .\load-tests\subscription_locustfile.py --host=http://localhost:5224
```

Run Accounting Service load test:

```powershell
locust -f .\load-tests\accounting_locustfile.py --host=http://localhost:5222
```

Run Exchange Proxy Service load test:

```powershell
locust -f .\load-tests\exchange_locustfile.py --host=http://localhost:5021
```

Open `http://localhost:8089`, use 50 users and spawn rate 5 users per second for the local demonstration.

## Docker Infrastructure

The repository includes Docker Compose for RabbitMQ, PostgreSQL, and Jaeger:

```powershell
docker compose up -d
```

The final demonstration uses simulated service behavior so the main test endpoints can be verified quickly on a local machine.
