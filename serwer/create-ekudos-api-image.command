docker build . -t rockudoapi:v1 --no-cache
docker run -d=false -p 5000:80 --name rockudoapicontainer4 rockudoapi:v1