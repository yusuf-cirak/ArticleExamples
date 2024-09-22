SEPARATOR="----------------------------------------"

echo $SEPARATOR
./prometheus/run.sh && echo $SEPARATOR && ./grafana/run.sh && echo $SEPARATOR && ./k6/run.sh
