import requests
from faker import Faker
import json

fake = Faker()

url = "http://localhost:64684/api/usuarioapi"

headers = {
    'Content-Type': "application/json",
    'Cache-Control': "no-cache"
    }

for _ in range(10000):
    nome = fake.name()
    payload = json.dumps({ 'nome': fake.name() })
    response = requests.request("POST", url, data=payload, headers=headers)
    print(response.status_code, nome)