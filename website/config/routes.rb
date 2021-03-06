Rails.application.routes.draw do


  resources :answers do
   collection do
      get :quiz
    end
  end

  resources :messes

  devise_for :teachers
  resources :questions
  resources :teachers do
    collection do
      get 'view_game_records'
      get 'view_school_verified_students'
      get 'view_courses'
      put 'request_subject/:subject_id'=> :request_subject
      get 'view_students_grades_in_questions'
      get 'view_in_game_grades'
      put 'send_message/:student_id'=> :send_message
      post 'submit/:student_id'=>:submit
      get 'view_contacts'
      get 'view_messages/:teacher_id'=>:view_messages
      get 'view_messages_students/:student_id'=>:view_messages_students
      get 'view_school_subjects'
    end
  end


  resources :subjects

  devise_for :school_admins 
  resources :school_admins do
    collection do
      get 'view_verified_students'
      get 'view_students_records'
      get 'view_requests'
      get 'view_teacher_requests'
      put 'remove_verified_student/:student_id' => :remove_verified_student
      get 'add_subject'
      get 'view_school_subjects'
      put 'accept_verification/:student_id' => :accept_verification
      put 'reject_verification/:student_id' => :reject_verification 
      put 'accept_teacher_verification/:teacher_id' => :accept_teacher_verification
      put 'reject_teacher_verification/:teacher_id' => :reject_teacher_verification
      get 'view_teachers_subjects'
      get 'view_verified_teachers'
      get 'view_subject_requests'
      put 'accept_subject_request/:teacherRequestSubject_id' => :accept_subject_request
      put 'reject_subject_request/:teacherRequestSubject_id' => :reject_subject_request        
      put 'remove_teacher_subject/:teacherRequestSubject_id' => :remove_teacher_subject
      put 'remove_verified_teacher/:teacher_id' => :remove_verified_teacher
      get 'view_in_game_grades'
      get 'view_students_grades_in_questions'
      get 'show_course/:course_id'=>:show_course
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
      get 'view_course_teachers/:subject_id'=> :view_course_teachers
      put 'send_message/:teacher_id'=> :send_message
      post 'submit/:teacher_id'=>:submit
      get 'take_quiz/:subject_id' => :take_quiz
      post 'Answer_quiz'=>:Answer_quiz
      get 'view_contacts'
      get 'view_messages/:student_id'=>:view_messages
      get 'view_messages_teacher/:teacher_id'=>:view_messages_teacher
      get 'view_teachers_in_school'
      get 'view_teacher_messages/:teacher_id'=>:view_teacher_messages
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
