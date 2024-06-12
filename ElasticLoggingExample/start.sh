# Set environment variable
export ELASTIC_PASSWORD="${ELASTIC_PASSWORD:-elasticdemo}"
export KIBANA_PASSWORD="${KIBANA_PASSWORD:-kibanademo}"
export ADMIN_USER_PASSWORD="${ADMIN_USER_PASSWORD:-admindemo}"
docker compose up --build --force-recreate -d
# docker rm -f set_elasticsearch_users