class HomeController < ApplicationController

	def index
		unless current_student
			redirect_to new_student_session_path
		end
	end


end