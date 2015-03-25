class ApplicationController < ActionController::Base
	before_filter :configure_permited_params , if: :devise_controller?
	  include SessionsHelper
	def configure_permited_params
		devise_parameter_sanitizer.for(:sign_up) << [:school , :grade , :student_name , :student_class]
	end


	
  # Prevent CSRF attacks by raising an exception.
  # For APIs, you may want to use :null_session instead.
  protect_from_forgery with: :exception
end
