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


	def reject_subject_request
		#get the request
		@request = TeacherRequestSubject.find(params[:teacherRequestSubject_id])
		#change verification of request
		@request.verified = false
		#change the flash method depending on the save status
		if @request.save
			redirect_to view_subject_requests_school_admins_path, alert: 'The request has not been verified successfully.'
			else
			redirect_to view_subject_requests_school_admins_path, notice: 'The request has been verified successfully.'
			end
	end


	def reject_teacher_verification
		# find the teachers who should been verified using the id in the params
		@teacher = Teacher.find(params[:teacher_id])
		@teacher.verified = nil
		# change the flash method according to the save action
		if @teacher.save
			redirect_to view_teacher_requests_school_admins_path, notice: 'The verification request has been removed successfully.'
		else
			redirect_to view_teacher_requests_school_admins_path, alert: 'The verification request has not been removed successfully.'
		end
	end


	def accept_teacher_verification
		# find the teachers who should been verified using the id in the params
		@teacher = Teacher.find(params[:teacher_id])
		@teacher.verified = true
		# change the flash method according to the save action
		if @teacher.save
			redirect_to view_teacher_requests_school_admins_path, notice: 'The teacher has been verified successfully.'
		else
			redirect_to view_teacher_requests_school_admins_path, alert: 'The teacher has not been verified successfully.'
		end
	end


	def accept_subject_request
		#get the request
		@request = TeacherRequestSubject.find(params[:teacherRequestSubject_id])
		#change verification of request
		@request.verified = true
		#change the flash method depending on the save status
		if @request.save
			redirect_to view_subject_requests_school_admins_path, notice: 'The request has been verified successfully.'
		else
			redirect_to view_subject_requests_school_admins_path, alert: 'The request has not been verified successfully.'
		end
	end


	def remove_teacher_subject
		@request = TeacherRequestSubject.find(params[:teacherRequestSubject_id])
		@request.destroy
		redirect_to view_teachers_subjects_school_admins_path
	end


	def view_subject_requests
		#get all requests from current school
		@allRequests = TeacherRequestSubject.where("school = ?", current_school_admin.school)
		#filter requests to only unverified ones
		@requests = @allRequests.where(verified: nil)
	end


	def view_teacher_requests
		# get the school admin from the url till now
		@current_admin = current_school_admin
		if @current_admin
			# get teachers of their school who are not verified to display their scores in the view
			@teachers = Teacher.where("school = ? AND verified = ?" , 
				@current_admin.school, false)
		else
			@teachers = {}
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


	def remove_verified_student
		@current_admin = current_school_admin
		@student = Student.find(params[:student_id])
		@student.verified = nil
		@student.save
		redirect_to view_verified_students_school_admins_path
	end


	def add_subject
		#create an instance of subject to pass it to the partial view local variables
		@subject = Subject.new
	end


	def view_school_subjects
		# show all subjects that the school admin has created
		@subjects = Subject.where(school_admin_id: current_school_admin.id)
	end


	def accept_verification
		# find the student who should been verified using the id in the params
		@student = Student.find(params[:student_id])
		@student.verified = true
		# change the flash method according to the save action
		if @student.save
			redirect_to view_requests_school_admins_path, notice: 'The student has been verified successfully.'
		else
			redirect_to view_requests_school_admins_path, alert: 'The student has not been verified successfully.'
		end
	end


	def reject_verification
		# find the student who should been unverified using the id in the params
		@student = Student.find(params[:student_id])
		@student.verified = nil
		# change the flash method according to the save action
		if @student.save
			redirect_to view_requests_school_admins_path, notice: 'The verification request has been removed successfully.'
		else
			redirect_to view_requests_school_admins_path, alert: 'The verification request has not been removed successfully.'
		end
	end


	def accept_school_admin
		# get the school admin from the id in the url
		@school_admin = SchoolAdmin.find(params[:id])
		# update the attribute
		@school_admin.verified = true
		# save 
		@school_admin.save
		# redirect to the dash board of the admin
		redirect_to admin_dashboard_path
	end


	def reject_school_admin
		# get the school admin from the id in the url
		@school_admin = SchoolAdmin.find(params[:id])
		# update the attribute
		@school_admin.verified = nil
		# save 
		@school_admin.save
		# redirect to the dash board of the admin
		redirect_to admin_dashboard_path
	end


	def view_teachers_subjects
		@current_school_admin = current_school_admin
		@verified_teachers = Teacher.where("school = ? AND verified = ?", @current_school_admin.school, true)
	end

	def view_verified_teachers
        @current_admin=current_school_admin
        if @current_admin
          @teachers = Teacher.where("school = ? AND verified = ?" ,
          @current_admin.school, true)
        else
        @teachers={}

        end

    end 


    def remove_verified_teacher
        @current_admin = current_school_admin
        @teacher = Teacher.find(params[:teacher_id])
        @teacher.verified = nil
        @teacher.save
        redirect_to view_verified_teachers_school_admins_path
    end


    def view_in_game_grades
    	@students = Student.where(school: current_school_admin.school)
    end
end