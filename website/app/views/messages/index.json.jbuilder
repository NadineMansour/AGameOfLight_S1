json.array!(@messages) do |message|
  json.extract! message, :id, :semail, :remail, :text
  json.url message_url(message, format: :json)
end
