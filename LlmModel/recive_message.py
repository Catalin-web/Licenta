import requests

url = "http://127.0.0.1:5000/generate-response"
obj = {
    "prompt": "USER: Generate a list of 5 dogs names. SERVER: ",
    "max_tokens": 100,
}

x = requests.post(url, json=obj)

print(x.text)
