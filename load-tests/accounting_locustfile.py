from locust import HttpUser, task, between

class AccountingServiceUser(HttpUser):
    wait_time = between(0.1, 0.5)

    @task(3)
    def get_accounts(self):
        self.client.get("/api/accounts")

    @task(2)
    def get_positions(self):
        self.client.get("/api/positions")

    @task(2)
    def get_ledger(self):
        self.client.get("/api/ledger")

    @task(1)
    def health(self):
        self.client.get("/api/health")
