class SchoolAdminsController < ApplicationController


	def view_verified_students
		# get the school admin
		@current_admin = current_school_admin
		# get the sorting method if available
		@method = params[:sort_method]
		if @current_admin
			@students = Student.where("school = ? AND verified = ?" , 
				@current_admin.school, true)
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
		else
			@students = {}
		end
	end


	def view_students_records
		# get the school admin from the url till now
		@current_admin = current_school_admin
		if @current_admin
			# get students of their school who are verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_admin.school, true)
		else
			@students = {}
		end
	end


	def view_requests
		# get the school admin from the url till now
		@current_admin = current_school_admin
		if @current_admin
			# get students of their school who are not verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_admin.school, false)
		else
			@students = {}
		end
	end


end