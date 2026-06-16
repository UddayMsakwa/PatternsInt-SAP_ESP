&#x20;SAP Copy Trading Platform



&#x20;Overview



This project is a simplified backend implementation of a copy trading platform for the Patterns and Integration assignment.



The system simulates how a trader's trade signal can be copied to follower accounts through a set of cooperating services.



The implementation focuses on demonstrating architectural and integration patterns rather than building a real brokerage system.



&#x20;Services



The solution contains the following services:



&#x20;Trader Service



Responsible for trader-related data and trade signal handling.



&#x20;Subscription Service



Responsible for managing follower subscriptions to trader accounts.



&#x20;Copy Engine Service



A background worker that simulates the copy trading saga. It processes trade copy logic and represents the orchestration part of the system.



&#x20;Exchange Proxy Service



A simulated exchange service. It accepts execution requests and returns simulated execution results.



&#x20;Accounting Service



A simulated accounting service. It exposes account, position, and ledger endpoints.



&#x20;Shared Contracts



Contains common DTOs, event contracts, command/query objects, and shared message structures.



&#x20;Shared Infrastructure



Contains common infrastructure helpers and extension points.



&#x20;Architecture



The simplified architecture is:



text

Trader Service

&#x20;     |

&#x20;     v

Subscription Service

&#x20;     |

&#x20;     v

Copy Engine Service

&#x20;     |

&#x20;     +----> Exchange Proxy Service

&#x20;     |

&#x20;     +----> Accounting Service





The system is implemented as multiple .NET services with separate responsibilities.



&#x20;Patterns Demonstrated



The project demonstrates the following patterns:



| Pattern               | Where Used                                                | Purpose                                                       |

| --------------------- | --------------------------------------------------------- | ------------------------------------------------------------- |

| Saga                  | Copy Engine Service                                       | Coordinates the simulated copy trading workflow.              |

| Outbox                | Trader and Subscription services                          | Stores integration-style messages together with local data.   |

| Event Sourcing        | Shared event store records and service history structures | Represents important business changes as events.              |

| CQRS                  | Shared command and query contracts                        | Separates write operations from read operations.              |

| Service Decomposition | All services                                              | Splits the system into independently understandable services. |

| Exchange Proxy        | Exchange Proxy Service                                    | Simulates external exchange integration.                      |



&#x20;Technology Stack



\* .NET 8

\* ASP.NET Core Web API

\* .NET Worker Service

\* Entity Framework Core

\* PostgreSQL-ready configuration

\* Docker Compose infrastructure

\* Locust load testing

\* Jaeger prepared for tracing



&#x20;Build



From the repository root:



powershell

dotnet restore .\\PatternsInt-SAP\_ESP.sln

dotnet build .\\PatternsInt-SAP\_ESP.sln





&#x20;Run Services



Run each service in a separate PowerShell window.



powershell

dotnet run --project .\\src\\SubscriptionService.Api

dotnet run --project .\\src\\AccountingService.Api

dotnet run --project .\\src\\ExchangeProxyService.Api

dotnet run --project .\\src\\CopyEngineService.Worker

```



&#x20;Smoke Testing



Smoke testing can be executed with:



powershell

Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

.\\scripts\\smoke-test.ps1





&#x20;Load Testing



Load testing was performed using Locust.



Example:



powershell

\& "C:\\Users\\udday\\AppData\\Roaming\\Python\\Python313\\Scripts\\locust.exe" -f .\\load-tests\\subscription\_locustfile.py --host=http://localhost:5224





Locust files are stored in:



text

load-tests/





&#x20;Testing Evidence



Testing documentation and screenshots are stored in:



text

docs/TestingAndResults.md

docs/testing/screenshots/





&#x20;Notes



This implementation is a local simulated version of a copy trading backend. It demonstrates the required architecture and patterns, but it does not claim to be a production-ready brokerage system.



A production version would require real message broker communication, stronger persistence guarantees, distributed deployment, authentication, account reconciliation, risk checks, monitoring, and extended load testing.



