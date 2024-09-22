CONFIG_FILE_PATH="prometheus/prometheus.yml"
CONFIG_MAP_NAME="prometheus-cm"
kubectl create configmap $CONFIG_MAP_NAME --from-file=$CONFIG_FILE_PATH --dry-run=client -o yaml | kubectl apply -f -


DEPLOYMENT_FILE_PATH="prometheus/prometheus-depl.yml"

kubectl apply -f $DEPLOYMENT_FILE_PATH

SERVICE_FILE_PATH="prometheus/prometheus-srv.yml"
kubectl apply -f $SERVICE_FILE_PATH

DEPLOYMENT_NAME="prometheus"

# Wait for Prometheus deployment to become ready
echo "Waiting for $DEPLOYMENT_NAME deployment to become ready..."
kubectl wait \
  --for=condition=available \
    deployment/$DEPLOYMENT_NAME \
  --timeout=120s