# Patterns Used

| Pattern | Service | Explanation |
|---|---|---|
| Microservices | All services | The system is split into independent services with separate responsibilities. |
| Outbox | Trader Service, Subscription Service | Trade signals and subscription events are written into outbox tables together with business data. |
| Saga | Copy Engine Service | The copy engine coordinates the multi-step process of finding followers, executing copied orders, and recording accounting results. |
| Event Sourcing | Shared Contracts | EventStoreRecord represents immutable event history for business actions. |
| CQRS | Shared Contracts | Command records and query records separate write actions from read actions. |
| Circuit Breaker concept | Exchange Proxy Service | The exchange proxy is isolated as a separate service where exchange delay/failure handling would be implemented. |
| Idempotency concept | Copy Engine / Accounting | Duplicate processing is considered in the design; a production version would store processed message identifiers. |

## Notes

This implementation is intentionally simplified for the assignment. It demonstrates the architectural patterns without building a complete stockbroker platform.
