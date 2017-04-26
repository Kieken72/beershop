
projectList=(
    "/src/Services/Catalog/Catalog.API"
    "/src/Web/WebMVC"
    "/src/Web/WebStatus
)

# Build SPA app
pushd $(pwd)/src/Web/WebSPA
npm rebuild node-sass
npm run build:prod

for project in "${projectList[@]}"
do
    echo -e "\e[33mWorking on $(pwd)/$project"
    echo -e "\e[33m\tRemoving old publish output"
    pushd $(pwd)/$project
    rm -rf obj/Docker/publish
    echo -e "\e[33m\tRestoring project"
    dotnet restore
    echo -e "\e[33m\tBuilding and publishing projects"
    dotnet publish -o obj/Docker/publish
    popd
done

# remove old docker images:
#images=$(docker images --filter=reference="beershop/*" -q)
#if [ -n "$images" ]; then
#    docker rm $(docker ps -a -q) -f
#    echo "Deleting beershop images in local Docker repo"
#    echo $images
#    docker rmi $(docker images --filter=reference="beershop/*" -q) -f
#fi

# No need to build the images, docker build or docker compose will
# do that using the images and containers defined in the docker-compose.yml file.
