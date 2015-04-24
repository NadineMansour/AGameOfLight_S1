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


	def view_school_verified_students
		@current_teacher = current_teacher
		# get the sorting method if available
		@method = params[:sort_method]
		@order = params[:order_method]
		if @current_teacher && @current_teacher.verified
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
			# sort only if there sre students and if the school admin wants to sort
			if @method && @students && @order
				if @method == "1" #sort by grade
					if @order == "1"
						@students = @students.order(grade: :asc)
					else
						@students = @students.order(grade: :desc)
					end
				elsif @method == "2" #sort by name
					if @order == "1"
						@students = @students.order(student_name: :asc)
					else
						@students = @students.order(student_name: :desc)
					end
				elsif @method == "3" #sort by signing up date
					if @order == "1"
						@students = @students.order(created_at: :asc)
					else
						@students = @students.order(created_at: :desc)
					end
				end
			end
		else
			@students = {}
		end
	end


		def view_requests
		# get the teacher from the url till now
		@current_teacher = current_teacher
		if @current_teacher
			# get students of their school who are not verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, false)
		else
			@teachers = {}
		end
	end


	def accept_verification
		# find the student who should been verified using the id in the params
		@student = Student.find(params[:student_id])
		@student.verified = true
		# change the flash method according to the save action
		if @student.save
			redirect_to view_requests_teachers_path, notice: 'The student has been verified successfully.'
		else
			redirect_to view_requests_teachers_path, alert: 'The student has not been verified successfully.'
		end
	end


	def reject_verification
		# find the student who should been unverified using the id in the params
		@student = Student.find(params[:student_id])
		@student.verified = nil
		# change the flash method according to the save action
		if @student.save
			redirect_to view_requests_teachers_path, notice: 'The verification request has been removed successfully.'
		else
			redirect_to view_requests_teachers_path, alert: 'The verification request has not been removed successfully.'
		end
	end
end