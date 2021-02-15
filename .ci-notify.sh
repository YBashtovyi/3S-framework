#!/bin/bash
curl \
    --data '{"chat_id":"'"$TELEGRAM_CHAT_ID"'","parse_mode":"html","text":"'"$1"'"}' \
    --header "Content-Type: application/json" \
    --request GET \
    https://api.telegram.org/bot$TELEGRAM_BOT_TOKEN/sendMessage