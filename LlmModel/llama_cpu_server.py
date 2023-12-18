from flask import Flask, request, jsonify
from llama_cpp import Llama

app = Flask(__name__)
model = None


@app.route("/generate-response", methods=["POST"])
def generate_response():
    global model
    try:
        data = request.get_json()
        # Check if the required fields are present in the JSON data
        if "max_tokens" in data and "prompt" in data:
            prompt = data["prompt"]
            max_tokens = int(data["max_tokens"])
            if model is None:
                model_path = "./llama-2-7b-chat.Q2_K.gguf"
                model = Llama(model_path=model_path)
            output = model(prompt, max_tokens=max_tokens, echo=True)
            response = output["choices"][0]["text"]

            return jsonify({"response": response})
        else:
            return jsonify({"error": "You need to pass max toxens and a prompt"}), 400

    except Exception as e:
        return jsonify({"Error": str(e)}), 400


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=False)
