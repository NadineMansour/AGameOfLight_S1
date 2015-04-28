class TeachersController < ApplicationController
	def show
	end


	def view_courses

   @sub = Subject.where(school: current_teacher.school)
   @taken_id=TeacherRequestSubject.where(teacher: current_teacher , verified: true ).pluck(:subject_id)
   @subjects=@sub.where.not(id:@taken_id) #i have all records with false not accepted and nill pending
   @not_accepted=TeacherRequestSubject.where(teacher: current_teacher , verified: false ).pluck(:subject_id)
   @subjects=@subjects.where.not(id:@not_accepted) 
   @not_yet=TeacherRequestSubject.where(teacher: current_teacher , verified: nil ).pluck(:subject_id)
   @subjects2=@subjects.where(id:@not_yet) #3shan azhr gambha pending 
   @subjects=@subjects.where.not(id:@not_yet) 

    end


  def  request_subject
     
     @request=TeacherRequestSubject.new
     @request.teacher_id=current_teacher.id
     @request.subject_id=params[:subject_id]
     @request.school=current_teacher.school
     #@request.school_admin_id=current_teacher.school_admin_id
     @request.save
     redirect_to view_courses_teachers_path
     
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
		@method = params[:filter_method]
		@search = params[:search]
		if @current_teacher && @current_teacher.verified
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
			# sort only if there sre students and if the school admin wants to sort
			if @method && @students && @search
				if @method == "1" #sort by grade
					@students = @students.where(grade: @search)
				elsif @method == "2" #sort by name
					@students = @students.where(student_name: @search)
				elsif @method == "3" #sort by signing up date
					@students = @students.where(created_at: @search)
				elsif @method == "4"
					@students = @students.where(student_class: @search)
				end
			end
		else
			@students = {}
		end
	end


	def view_students_grades_in_questions
		@student_id = params[:student_id]
		@teacher = current_teacher
		if @teacher.verified
			if @student_id
				@students = Student.where("id = ?", @student_id)
			else
				@students = Student.where("school = ? AND verified = ?", @teacher.school, true)
			end
		end
	end
end