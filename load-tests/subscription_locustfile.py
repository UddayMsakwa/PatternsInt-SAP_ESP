from locust import HttpUser, task, between

class SubscriptionServiceUser(HttpUser):
    wait_time = between(0.1, 0.5)

    @task(3)
    def get_subscriptions(self):
        self.client.get("/api/subscriptions")

    @task(1)
    def health(self):
        self.client.get("/api/health")
