from locust import HttpUser, task, between
import random
import uuid

class ExchangeProxyUser(HttpUser):
    wait_time = between(0.1, 0.5)

    @task(4)
    def execute_order(self):
        payload = {
            "subscriptionId": str(uuid.uuid4()),
            "accountNumber": "FOLLOWER-001",
            "symbol": "AAPL",
            "side": random.choice(["BUY", "SELL"]),
            "quantity": random.randint(1, 10),
            "price": round(random.uniform(150, 250), 2)
        }
        self.client.post("/api/executions", json=payload)

    @task(1)
    def get_results(self):
        self.client.get("/api/executions")

    @task(1)
    def health(self):
        self.client.get("/api/health")
