class SchoolAdminsController < ApplicationController


	def view_verified_students
		# get the school admin
		@current_admin = SchoolAdmin.find(params[:id])
		@students = Student.where("school = ? AND verified = ?" , 
			@current_admin.school, true)
		# get the sorting method if available
		@method = params[:sort_method]
		# sort only if there sre students and if the school admin wants to sort
		if @method && @students
			if @method == "1"
				@students = @students.order(grade: :asc)
			elsif @method == "2"
				@students = @students.order(grade: :desc)
			elsif @method == "3"
				@students = @students.order(student_name: :asc)
			elsif @method == "4"
				@students = @students.order(student_name: :desc)
			end
		end
	end


	def view_students_records
		# get the school admin from the url till now
		@current_admin = SchoolAdmin.find(params[:id])
		# get students of their school who are verified to display their scores in the view
		@students = Student.where("school = ? AND verified = ?" , 
			@current_admin.school, true)
	end


	def view_requests
		# get the school admin from the url till now
		@current_admin = SchoolAdmin.find(params[:id])
		# get students of their school who are not verified to display their scores in the view
		@students = Student.where("school = ? AND verified = ?" , 
			@current_admin.school, false)
	end


end