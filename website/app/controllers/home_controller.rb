class HomeController < ApplicationController

	def index
		if current_school_admin
			redirect_to current_school_admin
		elsif current_student
			redirect_to current_student
		else
			redirect_to new_student_session_path
		end
	end


end