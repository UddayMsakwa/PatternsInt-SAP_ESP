# Testing and Results

## Manual Functional Testing

The following services were started locally using `dotnet run`:

- Trader Service
- Subscription Service
- Accounting Service
- Exchange Proxy Service
- Copy Engine Service

The following endpoints were manually tested:

| Service | Endpoint | Expected Result |
|---|---|---|
| Subscription Service | `/api/subscriptions` | Returns active subscriptions as JSON. |
| Accounting Service | `/api/accounts` | Returns simulated accounts as JSON. |
| Accounting Service | `/api/positions` | Returns simulated positions as JSON. |
| Accounting Service | `/api/ledger` | Returns ledger entries as JSON. |
| Exchange Proxy Service | `/api/executions` | Returns execution history as JSON. |
| Exchange Proxy Service | `POST /api/executions` | Returns a successful simulated execution. |
| Copy Engine Service | Background worker logs | Shows saga processing and copied trade completion. |

## Load Testing

Load testing was prepared using Locust.

Recommended local configuration:

- Concurrent users: 50
- Spawn rate: 5 users per second
- Duration: 3 minutes

The following Locust files are provided:

- `load-tests/subscription_locustfile.py`
- `load-tests/accounting_locustfile.py`
- `load-tests/exchange_locustfile.py`

## Performance Summary

The implementation is a simplified local simulation. The services are expected to handle the local load test without failures because the workflow uses lightweight in-memory simulation for the final demonstration endpoints.

The assignment's target load is high: up to 1,000 active trader accounts, up to 10,000 subscriber accounts per trader, and up to 100 trades per trader per trading day. This local implementation demonstrates the architectural approach but does not claim production-grade validation at that full scale.

If the stated parameters are not met in a production implementation, likely bottlenecks would include:

- Copy Engine worker throughput
- database write speed for accounting events
- exchange execution latency
- local machine CPU and memory limits
- message broker throughput

Jaeger is included in the Docker Compose infrastructure so distributed tracing can be added to isolate slow service calls in a production version.
