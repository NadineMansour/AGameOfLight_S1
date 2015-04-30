json.array!(@answers) do |answer|
  json.extract! answer, :id, :belongs_to, :belongs_to, :ans, :correct
  json.url answer_url(answer, format: :json)
end
