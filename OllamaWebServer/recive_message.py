import requests

url = "http://localhost:11434/api/generate"
data = {
    "model": "mistral",
    "prompt": "Write me a 10 word poem",
    "stream": False,
}

response = requests.post(url, json=data)

if response.status_code == 200:
    json_response = response.json()
    print(json_response.get("response"))
else:
    print(f"Error: {response.status_code} - {response.text}")
