Rails.application.routes.draw do


  devise_for :students


  namespace :api, defaults: { format: :json } do
      resources :records do
        collection do
          post 'get_records_by_email'
          post 'save_record'
          get 'get_level'
          post 'user_login'
        end
      end
  end

  
  root 'home#index'
end
