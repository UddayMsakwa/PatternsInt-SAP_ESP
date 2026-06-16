# Architecture

The project implements a simplified copy trading backend using a microservice structure.

## Services

| Service | Responsibility |
|---|---|
| Trader Service | Manages trader records and trade signals. |
| Subscription Service | Manages follower-to-trader subscriptions. |
| Copy Engine Service | Simulates the saga that copies trader orders for followers. |
| Exchange Proxy Service | Simulates an external exchange and returns execution results. |
| Accounting Service | Simulates account balances, positions, and ledger records. |

## Runtime Flow

1. A trader sends a trade signal.
2. The signal is stored by Trader Service.
3. Copy Engine Service processes copy-trade work as a saga.
4. The saga finds follower subscriptions.
5. The saga sends copied orders to Exchange Proxy Service.
6. Successful execution is recorded by Accounting Service.

## Infrastructure

The repository includes Docker Compose configuration for RabbitMQ, PostgreSQL, and Jaeger. The submitted implementation uses a simulated workflow so the project can run locally without requiring a full production message broker setup.
