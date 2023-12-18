# Use an official Python runtime as a parent image
FROM python:3.10
WORKDIR /app

# Install lamma
COPY ./web_api.py /app/web_api.py
COPY llama-2-7b-chat.Q2_K.gguf /app/llama-2-7b-chat.Q2_K.gguf

#Copy requirements
COPY ./requirements.txt /app/requirements.txt

# Install pip requirements
RUN pip install --no-cache-dir --upgrade -r requirements.txt

# Expose port 5000 to the world outside this container
EXPOSE 5000

# Run app.py when the container launches
CMD ["gunicorn", "--bind", "0.0.0.0:5000", "web_api:app"]