
DATASOURCE_FILE_PATH="./grafana/provisioning/datasources/datasource.yml"
CONFIG_MAP_NAME="grafana-datasources-cm"
kubectl create configmap $CONFIG_MAP_NAME --from-file=$DATASOURCE_FILE_PATH --dry-run=client -o yaml | kubectl apply -f -


DEPLOYMENT_FILE_PATH="grafana/grafana-depl.yml"
kubectl apply -f $DEPLOYMENT_FILE_PATH

SERVICE_FILE_PATH="grafana/grafana-srv.yml"
kubectl apply -f $SERVICE_FILE_PATH


DEPLOYMENT_NAME="grafana"
# Wait for Grafana deployment to become ready
echo "Waiting for $DEPLOYMENT_NAME deployment to become ready..."
kubectl wait \
  --for=condition=available \
    deployment/$DEPLOYMENT_NAME \
  --timeout=120s