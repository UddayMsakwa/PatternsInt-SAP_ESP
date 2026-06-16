# Load Testing

Locust is used for lightweight local load testing.

Install:

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

Open http://localhost:8089 and use 50 users with a spawn rate of 5 users per second for the local test.
