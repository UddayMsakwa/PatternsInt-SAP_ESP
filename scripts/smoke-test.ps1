Write-Host "Testing Subscription Service..."
Invoke-RestMethod http://localhost:5224/api/health
Invoke-RestMethod http://localhost:5224/api/subscriptions

Write-Host "Testing Accounting Service..."
Invoke-RestMethod http://localhost:5222/api/health
Invoke-RestMethod http://localhost:5222/api/accounts
Invoke-RestMethod http://localhost:5222/api/positions
Invoke-RestMethod http://localhost:5222/api/ledger

Write-Host "Testing Exchange Proxy Service..."
Invoke-RestMethod http://localhost:5021/api/health
Invoke-RestMethod http://localhost:5021/api/executions

$body = @{
    subscriptionId = "11111111-1111-1111-1111-111111111111"
    accountNumber = "FOLLOWER-001"
    symbol = "AAPL"
    side = "BUY"
    quantity = 10
    price = 185.50
} | ConvertTo-Json

Invoke-RestMethod http://localhost:5021/api/executions -Method Post -Body $body -ContentType "application/json"

Write-Host "Smoke tests completed."
