#!/bin/bash
sudo echo "pulling the host docker box..."
sudo docker pull eduardoamparo/okapp-host
sudo echo "pulling the ng docker box..."
sudo docker pull eduardoamparo/okapp-ng 
sudo echo "starting the docker compose file..."
sudo docker-compose up -d
sudo echo "done"
