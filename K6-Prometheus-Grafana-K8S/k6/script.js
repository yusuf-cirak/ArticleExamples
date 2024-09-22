/** @format */

import http from "k6/http";
import { check, sleep } from "k6";
import { Counter, Rate, Trend } from "k6/metrics";

// Define custom metrics
const httpReqFailed = new Rate("http_req_failed");
const httpReqSuccess = new Rate("http_req_success");
const httpReqConnectDuration = new Trend("http_req_connect_duration");
const httpReqSendDuration = new Trend("http_req_send_duration");
const httpReqReceiveDuration = new Trend("http_req_receive_duration");
const httpReqWaitDuration = new Trend("http_req_wait_duration");
const httpReqBlockDuration = new Trend("http_req_block_duration");
const httpReqRedirects = new Counter("http_req_redirects");
const httpReqServerError = new Counter("http_req_server_error");
const httpReqClientError = new Counter("http_req_client_error");
const httpReqUnauthorized = new Counter("http_req_unauthorized");

// Define your request URLs
const urls = [
	"https://yusufcirak.net/",
	"https://chat.yusufcirak.net/",
	"https://stream.yusufcirak.net/api/streams/live/ysfcrk",
	"https://stream.yusufcirak.net/api/streams/live",
	"https://stream.yusufcirak.net/api/streams/following",
	"https://stream.yusufcirak.net/api/streams/recommended",
	"https://stream.yusufcirak.net/api/streams/blocked",
	"https://stream.yusufcirak.net/api/stream-follower-users/count/1b2b13dd-3f12-44c3-92dc-0aa17274ae70",
];

export let options = {
	duration: "10m", // Test duration of 10 minutes
	vus: 100, // 100 virtual users
};

export default function () {
	for (const url of urls) {
		const res = http.get(url);

		// Track metrics
		httpReqConnectDuration.add(res.timings.connecting);
		httpReqSendDuration.add(res.timings.sending);
		httpReqReceiveDuration.add(res.timings.receiving);
		httpReqWaitDuration.add(res.timings.waiting);
		httpReqBlockDuration.add(res.timings.blocked);
		httpReqRedirects.add(res.redirects || 0);

		// Check for success or failure and add global tags
		const success = check(res, { "status was 200": (r) => r.status === 200 });
		const serverError = check(res, {
			"status was 500": (r) => r.status === 500,
		});
		const clientError = check(res, {
			"status was 400": (r) => r.status === 400,
		});
		const unauthorized = check(res, {
			"status was 401": (r) => r.status === 401,
		});

		// Update metrics based on checks
		httpReqFailed.add(!success);
		httpReqSuccess.add(success);
		httpReqServerError.add(serverError);
		httpReqClientError.add(clientError);
		httpReqUnauthorized.add(unauthorized);

		sleep(1); // Mimic user pacing
	}
}
