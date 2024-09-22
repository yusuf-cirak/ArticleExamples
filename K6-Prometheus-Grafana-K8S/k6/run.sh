export JOB_NAME="k6-load-test"
export CONFIG_MAP_NAME="k6-script-cm"
export PROMETHEUS_REMOTE_WRITE_URL="http://prometheus:80/api/v1/write"
export SCRIPT_FILE_NAME="k6/script.js"

export TEST_ID=$(LC_ALL=C tr -dc 'a-zA-Z0-9' < /dev/urandom | head -c 8)

kubectl delete jobs.batch $JOB_NAME

kubectl create configmap $CONFIG_MAP_NAME --from-file=$SCRIPT_FILE_NAME --dry-run=client -o yaml | kubectl apply -f -


BATCH_PATH=$(pwd)/k6/k6-batch.yml
echo $BATCH_PATH
envsubst < $BATCH_PATH | kubectl apply -f -
