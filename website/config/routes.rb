Rails.application.routes.draw do

  resources :subjects

  devise_for :school_admins 
  resources :school_admins do
    collection do
      get 'view_verified_students'
      get 'view_students_records'
      get 'view_requests'
      put 'remove_verified_student/:student_id' => :remove_verified_student
      get 'add_subject'
      get 'view_school_subjects'
      put 'accept_verification/:student_id' => :accept_verification
      put 'reject_verification/:student_id' => :reject_verification
    end
  end
  devise_for :admin_users, ActiveAdmin::Devise.config
  ActiveAdmin.routes(self)
  devise_for :students
  resources :students


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
