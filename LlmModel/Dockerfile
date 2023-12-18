# Use an official Python runtime as a parent image
FROM python:3.8
WORKDIR /app

# Install lamma
COPY ./llama_cpu_server.py /app/llama_cpu_server.py
COPY llama-2-7b-chat.Q2_K.gguf /app/llama-2-7b-chat.Q2_K.gguf

# Install pip requirements
RUN pip install llama-cpp-python==0.2.23 Flask==3.0.0

# Expose port 5000 to the world outside this container
EXPOSE 5000

# Run app.py when the container launches
CMD ["python", "llama_cpu_server.py"]
