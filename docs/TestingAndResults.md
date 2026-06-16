# Testing and Results

## 1. Testing Objective

The objective of testing was to verify that the simplified copy trading platform works as an integrated microservice prototype. Testing focused on the following goals:

- confirming that all projects compile successfully;
- confirming that the main services start locally;
- confirming that HTTP endpoints return valid JSON responses;
- confirming that the Copy Engine worker executes the simulated saga workflow;
- checking basic behavior under concurrent load using Locust;
- identifying possible bottlenecks and future improvements.

The implementation is a local simulated version of the required copy trading backend. It demonstrates the architectural patterns required by the task, including microservices, outbox-style event storage, saga-style orchestration, CQRS-style separation, and event history concepts.

---

## 2. Build Verification

The full solution was built using the following command:

```powershell
dotnet build .\PatternsInt-SAP_ESP.sln
```

The solution built successfully. This confirms that all projects in the solution compile together and that the service references are valid.

**Evidence:**

- `docs/testing/screenshots/01-build-succeeded.png`

---

## 3. Services Started Locally

The following services were started locally using `dotnet run`:

| Service | Project |
|---|---|
| Trader Service | `src/TraderService.Api` |
| Subscription Service | `src/SubscriptionService.Api` |
| Accounting Service | `src/AccountingService.Api` |
| Exchange Proxy Service | `src/ExchangeProxyService.Api` |
| Copy Engine Service | `src/CopyEngineService.Worker` |

The Copy Engine Service runs as a background worker and periodically executes the simulated copy trading saga.

**Evidence:**

- `docs/testing/screenshots/03-copy-engine-worker.png`

---

## 4. Smoke Testing

Smoke testing was performed using the PowerShell script:

```powershell
.\scripts\smoke-test.ps1
```

Because PowerShell can block local scripts by default, the following command was used for the current terminal session:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

The smoke test checked that the main demonstration services were reachable and returned healthy responses.

| Service | Result |
|---|---|
| Subscription Service | Healthy |
| Accounting Service | Healthy |
| Exchange Proxy Service | Healthy |

The smoke test completed successfully.

**Evidence:**

- `docs/testing/screenshots/02-smoke-test-passed.png`

---

## 5. Manual Functional Testing

Manual functional testing was performed by opening the API endpoints in a browser and by using PowerShell requests where needed.

| Service | Endpoint | Method | Expected Result | Observed Result |
|---|---|---|---|---|
| Subscription Service | `/api/subscriptions` | GET | Returns subscriptions as JSON | Passed |
| Accounting Service | `/api/accounts` | GET | Returns simulated accounts as JSON | Passed |
| Accounting Service | `/api/positions` | GET | Returns simulated positions as JSON | Passed |
| Accounting Service | `/api/ledger` | GET | Returns ledger entries as JSON | Passed |
| Exchange Proxy Service | `/api/executions` | GET | Returns execution history as JSON | Passed |
| Exchange Proxy Service | `/api/executions` | POST | Returns a successful simulated execution | Passed |
| Copy Engine Service | Background worker logs | N/A | Shows saga processing | Passed |

For some GET endpoints, an empty JSON array such as `[]` is an acceptable result. It confirms that the route exists, the service is running, and the endpoint returns valid JSON even when no records have been created yet.

**Evidence:**

- `docs/testing/screenshots/04-exchange-proxy-json.png`
- `docs/testing/screenshots/05-subscription-json.png`
- `docs/testing/screenshots/06-accounting-json.png`

---

## 6. Copy Engine Saga Verification

The Copy Engine Service was tested by running:

```powershell
dotnet run --project .\src\CopyEngineService.Worker
```

The worker logs showed that the copy trading saga was being processed repeatedly. This verifies that the background service starts correctly and executes the simulated orchestration workflow.

The Copy Engine represents the saga coordinator. In the simplified implementation, it simulates the flow of receiving a trade signal, identifying followers, executing a copied trade, and recording the result.

**Evidence:**

- `docs/testing/screenshots/03-copy-engine-worker.png`

---

## 7. Load Testing Setup

Load testing was performed using Locust.

Locust was installed using:

```powershell
pip install locust
```

On this Windows machine, `locust.exe` was installed in the Python user scripts directory, so it was executed using the full path:

```powershell
& "C:\Users\udday\AppData\Roaming\Python\Python313\Scripts\locust.exe" --version
```

The following Locust test files are included in the project:

| Service | Locust File | Host Used |
|---|---|---|
| Subscription Service | `load-tests/subscription_locustfile.py` | `http://localhost:5224` |
| Accounting Service | `load-tests/accounting_locustfile.py` | `http://localhost:5222` |
| Exchange Proxy Service | `load-tests/exchange_locustfile.py` | `http://localhost:5021` |

Recommended local test configuration:

| Parameter | Value |
|---|---:|
| Concurrent users | 50 |
| Spawn rate | 5 users per second |
| Duration | 2 to 3 minutes per service |

---

## 8. Load Testing Commands

### Subscription Service

```powershell
& "C:\Users\udday\AppData\Roaming\Python\Python313\Scripts\locust.exe" `
  -f .\load-tests\subscription_locustfile.py `
  --host=http://localhost:5224
```

### Accounting Service

```powershell
& "C:\Users\udday\AppData\Roaming\Python\Python313\Scripts\locust.exe" `
  -f .\load-tests\accounting_locustfile.py `
  --host=http://localhost:5222
```

### Exchange Proxy Service

```powershell
& "C:\Users\udday\AppData\Roaming\Python\Python313\Scripts\locust.exe" `
  -f .\load-tests\exchange_locustfile.py `
  --host=http://localhost:5021
```

The Locust web interface was opened at:

```text
http://localhost:8089
```

---

## 9. Load Testing Results

The local Locust tests were executed against the Subscription Service, Accounting Service, and Exchange Proxy Service.

| Service | Users | Spawn Rate | Duration | Result |
|---|---:|---:|---:|---|
| Subscription Service | 50 | 5 users/s | 2-3 minutes | Completed |
| Accounting Service | 50 | 5 users/s | 2-3 minutes | Completed |
| Exchange Proxy Service | 50 | 5 users/s | 2-3 minutes | Completed |

The services continued returning valid JSON responses during the load tests. No critical runtime errors were observed during the local test runs.

**Evidence:**

- `docs/testing/screenshots/07-locust-subscription.png`
- `docs/testing/screenshots/08-locust-accounting.png`
- `docs/testing/screenshots/09-locust-exchange.png`

---

## 10. Performance Summary

The task describes the following target load conditions:

- up to 1,000 active trader accounts;
- up to 10,000 subscriber accounts per trader;
- up to 100 trades per trader per trading day;
- copied orders should be sent to the exchange no later than 5 minutes after the original trade.

The current implementation is a coursework prototype and local simulation. It demonstrates the system architecture and the required integration patterns, but it does not claim full production-scale validation of the maximum target load.

The local load test confirms that the demonstration endpoints remain stable under basic concurrent local traffic. However, production-level validation would require a distributed environment, real message broker testing, persistent database tuning, telemetry, and longer test runs.

---

## 11. Bottleneck Analysis

If the full stated load targets were not met in a production implementation, the most likely bottlenecks would be:

- Copy Engine worker throughput when processing many follower accounts;
- database write speed for accounting events and ledger updates;
- message broker throughput if real asynchronous messaging is enabled;
- Exchange Proxy latency and retry behavior;
- network latency between services;
- local machine CPU and memory limitations;
- inefficient database indexes for subscription lookup by trader.

To improve scalability, the following changes would be recommended:

- run multiple Copy Engine worker instances;
- shard copy processing by trader identifier;
- index subscriptions by trader account and active status;
- use a durable message broker such as RabbitMQ for event delivery;
- tune PostgreSQL indexes and connection pooling;
- apply back-pressure and retry policies;
- use distributed tracing to measure cross-service latency.

---

## 12. Monitoring and Tracing

Jaeger is included in the Docker Compose infrastructure. In a production-ready version, OpenTelemetry traces could be sent to Jaeger from each service.

Tracing would help identify slow service calls across:

- Trader Service;
- Subscription Service;
- Copy Engine Service;
- Exchange Proxy Service;
- Accounting Service.

This would be especially useful for understanding where delays happen during the copy trading workflow.

---

## 13. Final Conclusion

The project was successfully tested as a simplified local copy trading platform backend.

The implementation demonstrates:

- multiple service structure;
- simulated copy trading workflow;
- Copy Engine saga processing;
- Exchange Proxy simulation;
- Accounting simulation;
- smoke testing;
- manual API testing;
- load testing with Locust;
- performance and bottleneck analysis.

The system is suitable as a coursework prototype and provides a clear foundation for a more production-ready distributed implementation.
