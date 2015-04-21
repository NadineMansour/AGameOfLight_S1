class TeachersController < ApplicationController
	def show
	end


	def view_game_records
		@current_teacher = current_teacher
		if @current_teacher && @current_teacher.verified
			# get students of their school who are verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
		else
			@students = {}
		end
	end
end