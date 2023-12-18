# Use an official Python runtime as a parent image
FROM python:3.10 as builder
WORKDIR /app

# Copy the necessary files
COPY ./web_api.py /app/web_api.py
COPY ./requirements.txt /app/requirements.txt
COPY /llama-2-7b-chat.Q2_K.* /app/llama-2-7b-chat.Q2_K.gguf

# Check if the model file exists locally
RUN test -e llama-2-7b-chat.Q2_K.gguf || \
wget -O llama-2-7b-chat.Q2_K.gguf https://huggingface.co/TheBloke/Llama-2-7B-Chat-GGUF/resolve/main/llama-2-7b-chat.Q2_K.gguf?download=true

# Install pip requirements
RUN pip install --no-cache-dir --upgrade -r requirements.txt

# Second stage
FROM python:3.10
WORKDIR /app

# Copy only necessary files from the builder stage
COPY --from=builder /app/web_api.py /app/web_api.py
COPY --from=builder /app/requirements.txt /app/requirements.txt
COPY --from=builder /app/llama-2-7b-chat.Q2_K.gguf /app/llama-2-7b-chat.Q2_K.gguf

# Install pip requirements
RUN pip install --no-cache-dir --upgrade -r requirements.txt

# Expose port 5000 to the world outside this container
EXPOSE 5000

# Run app.py when the container launches
CMD ["gunicorn", "--bind", "0.0.0.0:5000", "web_api:app"]
