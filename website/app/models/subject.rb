class Subject < ActiveRecord::Base
	#associations
	belongs_to :school_admin
	#validations
	validates :code ,uniqueness: { scope: [:school_admin_id] }
	has_many :teacher_request_subjects
	has_many :questions
end
