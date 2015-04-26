Rails.application.routes.draw do


  devise_for :teachers
  resources :questions
  resources :teachers do
    collection do
      get 'view_game_records'
      get 'view_school_verified_students'
      get 'view_courses'
      put 'request_subject/:subject_id'=> :request_subject
      get 'view_students_grades_in_questions'
    end
  end


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
      get 'view_teachers_subjects'
    end
    member do
      get 'accept_school_admin'
      get 'reject_school_admin'
    end
  end
  devise_for :admin_users, ActiveAdmin::Devise.config
  ActiveAdmin.routes(self)
  devise_for :students
  resources :students do
    collection do
      get 'view_courses'
      get 'show_course'
    end
  end



  namespace :api, defaults: { format: :json } do
      resources :records do
        collection do
          post 'get_records_by_email'
          post 'save_record'
          get 'get_level'
          post 'user_login'
          post 'save_answer'
        end
      end
  end

  
  root 'home#index'
end
